using UnityEngine;
using GameCreator.Runtime.Characters;
using System.Threading.Tasks;
using SimpleExtentions.Runtime.Common;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [AddComponentMenu("Game Creator/Simple Extensions/Simple Character Melee")]

    [Icon(Paths.PATH + "SimpleMelee/Editor/MeleeGizmo.png")]

    public class SimpleMeleeCharacter : MonoBehaviour
    {
        [SerializeField] public SimpleMeleeWeapon m_Weapon;
        private Character m_Character;
        private GameObject p_Weapon = null;
        private bool p_IsAttacking = false;
        private void Start()
        {
            m_Character = this.GetComponent<Character>();
            if (m_Weapon)
                this.Equip();
        }

        public void CharacterAttack()
        {
            if (m_Weapon)
            {
                if (!this.p_IsAttacking)
                    this.Attack(this.m_Character, p_Weapon);
            }
        }
        public bool IsAttacking()
        {
            return p_IsAttacking;
        }

        public void Equip()
        {
            p_Weapon = m_Character.Props.AttachPrefab(m_Weapon.Bone, m_Weapon.WeaponPrefab,m_Weapon.LocationOffset, m_Weapon.RotationOffset);
        }
        public void UnEquip()
        {
            p_Weapon.GetComponent<Rigidbody>().isKinematic = false;
            p_Weapon.GetComponent<Collider>().enabled = true;
            p_Weapon.GetComponent<Collider>().isTrigger = false;
            m_Character.Props.RemoveAtBone(m_Weapon.Bone);

            m_Weapon = null;
        }
        public bool HasWeapon()
        {
            if(this.m_Weapon == null) return false;
            return true;
        }

        private async void Attack(Character character, GameObject weapon)
        {
            Collider collider = weapon.GetComponent<Collider>();
            AnimationClip animClip = m_Weapon.AnimationClip;
            AvatarMask avatarMask = m_Weapon.AvatarMask;
            float animTime = animClip.length;
            collider.enabled = true;
            p_IsAttacking = true;
            ConfigGesture configuration = new ConfigGesture(0, animTime / 1, 1, m_Weapon.UseRootMotion, 0.1f, 0.1f);

            Task gestureTask = character.Gestures.CrossFade(animClip, avatarMask, BlendMode.Blend, configuration, true);

            await gestureTask;
            p_IsAttacking = false;
            if(collider != null) collider.enabled = false;
        }

    }
}