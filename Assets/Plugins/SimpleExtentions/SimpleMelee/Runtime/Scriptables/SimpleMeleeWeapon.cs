using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;
using SimpleExtentions.Runtime.Common;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [CreateAssetMenu(
        fileName = "Simple Melee Weapon",
        menuName = "Game Creator/Simple Extensions/Simple Melee Weapon"
    )]

    [Icon(Paths.PATH + "SimpleMelee/Editor/SwordGizmo.png")]

    [Serializable]
    public class SimpleMeleeWeapon : ScriptableObject
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_WeaponPrefab = GetGameObjectInstance.Create();
        [SerializeField] private AnimationClip m_AnimationClip;
        [SerializeField] private AvatarMask m_AvatarMask;
        [SerializeField] private bool m_UseRootMotion;
        [SerializeField] private Bone m_Bone = new Bone(HumanBodyBones.RightHand);
        [SerializeField] private Vector3 m_Position = new Vector3();
        [SerializeField] private Vector3 m_Rotation;

        private Args m_Args;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameObject WeaponPrefab => this.m_WeaponPrefab.Get(m_Args);
        public AnimationClip AnimationClip => this.m_AnimationClip;
        public AvatarMask AvatarMask => this.m_AvatarMask;
        public bool UseRootMotion => this.m_UseRootMotion;
        public Bone Bone => this.m_Bone;
        public Vector3 LocationOffset => this.m_Position;
        public Quaternion RotationOffset => Quaternion.Euler(this.m_Rotation);
    }
}