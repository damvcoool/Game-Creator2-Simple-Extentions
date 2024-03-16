using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace GameCreator.Runtime.Characters
{
    [Title("Point & Click Alternative")]
    [Image(typeof(IconLocationDrop), ColorTheme.Type.Blue)]

    [Category("Point & Click Alternative")]
    [Description(
        "Moves the Player where the pointer's position clicks from the Main Camera's perspective, providing options for UI passthrough"
    )]

    [Serializable]
    public class UnitPlayerPointClickAlternative : TUnitPlayer
    {
        private const int BUFFER_SIZE = 32;

        // RAYCAST COMPARER: ----------------------------------------------------------------------

        private static readonly RaycastComparer RAYCAST_COMPARER = new RaycastComparer();

        private class RaycastComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit a, RaycastHit b)
            {
                return a.distance.CompareTo(b.distance);
            }
        }

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private InputPropertyButton m_InputMove;

        [SerializeField] private LayerMask m_LayerMask;

        [SerializeField] private PropertyGetInstantiate m_Indicator;

        [SerializeField] private PropertyGetBool m_PassthroughUI = GetBoolFalse.Create;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private RaycastHit[] m_HitBuffer;

        [NonSerialized] private bool m_Press;
        [NonSerialized] private Location m_Location;
        [NonSerialized] private Args args;

        // INITIALIZERS: --------------------------------------------------------------------------

        public UnitPlayerPointClickAlternative()
        {
            this.m_LayerMask = Physics.DefaultRaycastLayers;
            this.m_Indicator = new PropertyGetInstantiate
            {
                usePooling = true,
                size = 5,
                hasDuration = true,
                duration = 1f
            };

            this.m_InputMove = InputButtonMousePress.Create();
        }

        public override void OnStartup(Character character)
        {
            base.OnStartup(character);
            this.m_InputMove.OnStartup();
        }

        public override void OnDispose(Character character)
        {
            base.OnDispose(character);
            this.m_InputMove.OnDispose();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            this.m_HitBuffer = new RaycastHit[BUFFER_SIZE];

            this.m_InputMove.RegisterStart(this.OnStartPointClick);
            this.m_InputMove.RegisterPerform(this.OnPerformPointClick);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            this.m_HitBuffer = Array.Empty<RaycastHit>();

            this.m_InputMove.ForgetStart(this.OnStartPointClick);
            this.m_InputMove.ForgetPerform(this.OnPerformPointClick);

            this.Character.Motion?.MoveToDirection(Vector3.zero, Space.World, 0);
        }

        // UPDATE METHODS: ------------------------------------------------------------------------

        public override void OnUpdate()
        {
            base.OnUpdate();
            this.m_InputMove.OnUpdate();

            GameObject user = this.Character.gameObject;
            if (!this.m_Location.HasPosition(user)) return;

            Vector3 position = this.m_Location.GetPosition(user);

#if UNITY_EDITOR
            Debug.DrawLine(position, position + Vector3.up * 5f);
#endif

            this.Character.Motion?.MoveToLocation(this.m_Location, 0.1f, null, 0);
            if (this.m_Press) this.m_Indicator.Get(user, position, Quaternion.identity);

            this.m_Press = false;
            this.m_Location = Location.None;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnStartPointClick()
        {
            if (!this.Character.IsPlayer) return;
            if (!this.Character.Player.IsControllable) return;

            this.m_Press = true;
        }

        private void OnPerformPointClick()
        {
            if (!this.Character.IsPlayer) return;
            if (!this.m_IsControllable) return;

            // Added Manually
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject() && !m_PassthroughUI.Get(args))
            {
                var currentModule = (InputSystemUIInputModule)EventSystem.current.currentInputModule;

                var hoveredGameObject = currentModule.GetLastRaycastResult(Mouse.current.deviceId).gameObject;

                if (hoveredGameObject.GetComponent<RectTransform>() != null)
                {
                    // Optional cursor modification here
                    return;
                }
            }
            // End of customization.

            Camera camera = ShortcutMainCamera.Get<Camera>();

            Ray ray = camera.ScreenPointToRay(Application.isMobilePlatform
                ? Touchscreen.current.primaryTouch.position.ReadValue()
                : Mouse.current.position.ReadValue()
            );

            int hitCount = Physics.RaycastNonAlloc(
                ray, this.m_HitBuffer,
                Mathf.Infinity, this.m_LayerMask,
                QueryTriggerInteraction.Ignore
            );

            Array.Sort(this.m_HitBuffer, 0, hitCount, RAYCAST_COMPARER);

            for (int i = 0; i < hitCount; ++i)
            {
                int colliderLayer = this.m_HitBuffer[i].transform.gameObject.layer;
                if ((colliderLayer & LAYER_UI) > 0) return;

                if (this.m_HitBuffer[i].transform.IsChildOf(this.Transform)) continue;

                Vector3 point = this.m_HitBuffer[i].point;
                this.m_Location = new Location(point);

                this.InputDirection = Vector3.Scale(
                    point - this.Character.transform.position,
                    Vector3Plane.NormalUp
                );

                return;
            }
        }

        // STRING: --------------------------------------------------------------------------------

        public override string ToString() => "Point & Click";
    }
}