using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GameCreator.Runtime.Characters.IK
{
    [Title("Simple Aim Weapon")]
    [Category("Simple Aim Weapon")]
    [Image(typeof(IconEye), ColorTheme.Type.Green)]

    [Description("IK System to allow Simple Shooter Gun to point in the direction the camera is pointing to")]

    [Serializable]
    public class AimTrack : TRigAnimationRigging
    {
        private class LookTargets : List<ILookTrack>
        {
            public ILookTrack Get(Vector3 target)
            {
                float minDistance = Mathf.Infinity;
                ILookTrack minLookTrack = null;

                foreach (ILookTrack lookTrack in this)
                {
                    if (lookTrack == null) continue;
                    if (!lookTrack.Exists) continue;

                    float distance = Vector3.Distance(target, lookTrack.Position);
                    if (!(distance < minDistance)) continue;

                    minLookTrack = lookTrack;
                    minDistance = distance;
                }

                return minLookTrack;
            }
        }

        private class LookLayers : SortedDictionary<int, LookTargets>
        { }
        // CONSTANTS: -----------------------------------------------------------------------------

        public const string RIG_NAME = "Simple Aim Weapon";
        private const float HORIZON = 10f;

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private float m_TrackSpeed = 270f;
        [SerializeField] private float m_MaxAngle = 120f;

        [SerializeField, Range(0f, 1f)] private float m_RHandWeight = 1f;

        // MEMBERS: -------------------------------------------------------------------------------

        private float m_WeightTarget;
        private Transform m_LookHandle;
        private Transform m_LookPoint;

        private readonly LookLayers m_Layers = new LookLayers();

        // PROPERTIES: ----------------------------------------------------------------------------
        public override string Title => "Aim Weapon";
        public override string Name => RIG_NAME;
        public override bool RequiresHuman => true;

        protected override float WeightTarget => this.m_WeightTarget;
        protected override float WeightSmoothTime => 0.35f;

        private MultiAimConstraint ConstraintHand { get; set; }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetTarget<T>(T look) where T : ILookTrack
        {
            if (look == null) return;
            if (!this.m_Layers.ContainsKey(look.Layer))
            {
                this.m_Layers[look.Layer] = new LookTargets();
            }

            if (this.m_Layers[look.Layer].Contains(look)) return;

            this.m_Layers[look.Layer].Add(look);
        }

        public void RemoveTarget<T>(T look) where T : ILookTrack
        {
            if (look == null) return;
            if (this.m_Layers.TryGetValue(look.Layer, out LookTargets targets))
            {
                targets.Remove(look);
            }
        }

        // IMPLEMENT METHODS: ---------------------------------------------------------------------
        protected override bool DoUpdate(Character character)
        {
            bool rebuildGraph = base.DoUpdate(character);

            this.ConstraintHand.data.sourceObjects.SetWeight(0, this.m_RHandWeight);

            ILookTrack lookTrackTarget = this.GetLookTrackTarget(character);

            Vector3 targetPosition = character.Eyes + character.transform.forward * HORIZON;
            Vector3 targetDirection;

            this.m_LookHandle.position = character.Eyes;

            if (lookTrackTarget != null && lookTrackTarget.Exists)
            {
                this.m_WeightTarget = 1f;
                targetPosition = lookTrackTarget.Position;

                Vector3 characterDirection = character.transform.forward;
                targetDirection = targetPosition - character.Eyes;

                float angle = Vector3.Angle(characterDirection, targetDirection);
                if (angle > this.m_MaxAngle)
                {
                    this.m_WeightTarget = 0f;
                    targetDirection = character.transform.forward;
                }
            }
            else
            {
                this.m_WeightTarget = 0f;
                targetDirection = character.transform.forward;
            }

            this.m_LookHandle.rotation = Quaternion.RotateTowards(
                this.m_LookHandle.rotation,
                Quaternion.LookRotation(targetDirection, Vector3.up),
                character.Time.DeltaTime * this.m_TrackSpeed
            );

            Debug.DrawLine(character.Eyes, targetPosition, Color.red);
            Debug.DrawLine(character.Eyes, this.m_LookPoint.position, Color.blue);

            return rebuildGraph;
        }
        protected override void OnBuildRigLayer(Character character)
        {
            if (this.m_LookHandle == null || this.m_LookPoint == null)
            {
                if (this.m_LookHandle != null) UnityEngine.Object.Destroy(this.m_LookHandle.gameObject);
                if (this.m_LookPoint != null) UnityEngine.Object.Destroy(this.m_LookPoint.gameObject);

                GameObject handle = new GameObject(RIG_NAME + "Handle");
                GameObject point = new GameObject(RIG_NAME + "Point");

                handle.hideFlags = HideFlags.HideAndDontSave;
                point.hideFlags = HideFlags.HideAndDontSave;

                this.m_LookHandle = handle.transform;
                this.m_LookHandle.position = character.Eyes;

                this.m_LookPoint = point.transform;
                this.m_LookPoint.SetParent(this.m_LookHandle);
                this.m_LookPoint.localPosition = Vector3.forward * HORIZON;
            }

            this.ConstraintHand = this.CreateConstraint(RIG_NAME + "RightHand", character,HumanBodyBones.RightHand, this.m_RHandWeight,new Vector3Int(1, 1, 1));

        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        private ILookTrack GetLookTrackTarget(Character character)
        {
            foreach (KeyValuePair<int, LookTargets> entryLayer in this.m_Layers)
            {
                ILookTrack target = entryLayer.Value.Get(character.Eyes);
                if (target != null && target.Exists) return target;
            }

            return null;
        }
        private MultiAimConstraint CreateConstraint(string name, Character character, HumanBodyBones bone, float weight, Vector3Int constraintAxis)
        {
            Transform boneTransform = character.Animim.Animator.GetBoneTransform(bone);
            if (boneTransform == null) return null;

            GameObject container = new GameObject(name);
            container.transform.SetParent(this.RigLayer.rig.transform);
            container.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            MultiAimConstraint constraint = container.AddComponent<MultiAimConstraint>();
            constraint.data.constrainedObject = boneTransform;
            constraint.data.aimAxis = MultiAimConstraintData.Axis.Y;
            constraint.data.upAxis = MultiAimConstraintData.Axis.X_NEG;

            constraint.data.constrainedXAxis = constraintAxis.x != 0;
            constraint.data.constrainedYAxis = constraintAxis.y != 0;
            constraint.data.constrainedZAxis = constraintAxis.z != 0;

            constraint.data.limits = new Vector2(
                -this.m_MaxAngle / 2f,
                this.m_MaxAngle / 2f
            );

            WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
            sourceObjects.Insert(0, new WeightedTransform(this.m_LookPoint, weight));

            constraint.data.sourceObjects = sourceObjects;
            return constraint;
        }
        // GIZMOS: --------------------------------------------------------------------------------

        protected override void DoDrawGizmos(Character character)
        {
            base.DoDrawGizmos(character);
            Gizmos.color = Color.cyan;

            if (this.m_LookPoint == null) return;

            Gizmos.DrawWireCube(
                this.m_LookPoint.position,
                Vector3.one * 0.1f
            );

            if (this.ConstraintHand == null) return;
            if (this.ConstraintHand.data.constrainedObject == null) return;

            Gizmos.DrawLine(
                this.ConstraintHand.data.constrainedObject.position,
                this.m_LookPoint.position
            );
        }
    }
}
