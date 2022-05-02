using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine.Playables;
using System.Threading.Tasks;
using UnityEngine;

namespace DM_Customization.Runtime
{
    [CreateAssetMenu(
        fileName = "Simple Weapon",
        menuName = "Custom/Simple Weapon"
    )]

    [Icon("")]

    [Serializable]
    public class SimpleWeapon : ScriptableObject
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_WeaponPrefab = GetGameObjectInstance.Create();
        [SerializeField] private PropertyGetDecimal m_AttackPower = new PropertyGetDecimal(5);
        [SerializeField] private AnimationClip m_AnimationClip;
        [SerializeField] private AvatarMask m_AvatarMask;
        [SerializeField] private bool m_UseRootMotion;
        [SerializeField] private Bone m_Bone = new Bone(HumanBodyBones.RightHand);
        [SerializeField] private Vector3 m_Position = new Vector3();
        [SerializeField] private Vector3 m_Rotation = new Vector3();
        private Args m_Args;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameObject WeaponPrefab => this.m_WeaponPrefab.Get(m_Args);
        public float AttackPower => (float)this.m_AttackPower.Get(m_Args);
        public AnimationClip AnimationClip => this.m_AnimationClip;
        public Bone Bone => this.m_Bone;
        public Vector3 LocationOffset => this.m_Position;
        public Vector3 RotationOffset => this.m_Rotation;

        public async void Attack(Character character, GameObject weapon)
        {
            Collider collider = weapon.GetComponent<Collider>();
            float time = m_AnimationClip.length;
            collider.enabled = true;

            ConfigGesture configuration = new ConfigGesture(0, this.m_AnimationClip.length / 1, 1, this.m_UseRootMotion, 0.1f, 0.1f);

            Task gestureTask = character.Gestures.CrossFade(this.m_AnimationClip, this.m_AvatarMask, BlendMode.Blend, configuration, false);

            await gestureTask;

            collider.enabled = false;
        }
        public GameObject Equip(Character character)
        {
            GameObject weapon = character.Props.Attach(this.m_Bone, this.m_WeaponPrefab.Get(m_Args), this.m_Position, Quaternion.Euler(this.m_Rotation));
            return weapon;
        }
        public void UnEquip(Character character)
        {
            character.Props.Remove(this.m_WeaponPrefab.Get(m_Args));
        }
    }
}