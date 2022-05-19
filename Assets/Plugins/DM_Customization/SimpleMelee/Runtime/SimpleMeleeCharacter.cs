using UnityEngine;
using GameCreator.Runtime.Characters;
using System.Threading.Tasks;

namespace DM_Customization.Runtime
{
    [AddComponentMenu("Game Creator/Custom/Simple Character Melee")]

    [Icon(DMPaths.DMPATH + "SimpleMelee/Editor/MeleeGizmo.png")]

    public class SimpleMeleeCharacter : MonoBehaviour
    {
        [SerializeField] public SimpleMeleeWeapon m_Weapon;
        private Character m_Character;
        private GameObject p_Weapon = null;
        private bool isAttacking = false;
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
                if (!this.isAttacking)
                    this.Attack(this.m_Character, p_Weapon);
            }
        }
        public bool IsAttacking()
        {
            return isAttacking;
        }

        public void Equip()
        {
            p_Weapon = m_Character.Props.Attach(m_Weapon.Bone, m_Weapon.WeaponPrefab,m_Weapon.LocationOffset, m_Weapon.RotationOffset);
        }
        public void UnEquip()
        {
            m_Character.Props.Remove(m_Weapon.WeaponPrefab);
            m_Weapon = null;
        }

        private async void Attack(Character character, GameObject weapon)
        {
            Collider collider = weapon.GetComponent<Collider>();
            AnimationClip animClip = m_Weapon.AnimationClip;
            AvatarMask avatarMask = m_Weapon.AvatarMask;
            float animTime = animClip.length;
            collider.enabled = true;
            isAttacking = true;
            ConfigGesture configuration = new ConfigGesture(0, animTime / 1, 1, m_Weapon.UseRootMotion, 0.1f, 0.1f);

            Task gestureTask = character.Gestures.CrossFade(animClip, avatarMask, BlendMode.Blend, configuration, true);

            await gestureTask;
            isAttacking = false;
            collider.enabled = false;
        }

    }
}