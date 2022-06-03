using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
    [AddComponentMenu("Game Creator/Custom/Simple Character Melee")]
    public class SimpleShooterCharacter : MonoBehaviour
    {
        [SerializeField] private SimpleShooterGun m_SimpleGun;

        private Character m_Character;
        private GameObject p_Gun = null;
        private bool p_IsAiming = false;
        private bool p_IsShooting = false;


        private void Start()
        {

            m_Character = this.GetComponent<Character>();
            if (m_SimpleGun)
                this.Equip();
        }
         

        public void CharacterShoot()
        {
            if(this.p_IsAiming && !this.p_IsShooting)
            {
                this.Shoot(this.m_Character, p_Gun);
            }
        }

        public void CharacterAim()
        {

        }

        public void Equip()
        {
            p_Gun = m_Character.Props.Attach(m_SimpleGun.Bone, m_SimpleGun.GunPrefab, m_SimpleGun.LocationOffset, m_SimpleGun.RotationOffset);
        }

        public void UnEquip()
        {
            m_Character.Props.Remove(m_SimpleGun.GunPrefab);
            m_SimpleGun = null;
        }

        private async void Shoot(Character character, GameObject gun)
        {
            Debug.Log($"{character.name} is Getting Ready");

            await Task.Delay(5000);

            Debug.Log($"{character.name} did BANG! BANG! with {gun.name}");
        }

    }
}