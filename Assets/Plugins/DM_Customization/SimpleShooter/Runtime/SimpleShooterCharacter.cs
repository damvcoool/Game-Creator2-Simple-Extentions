using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;
//Debug
using UnityEngine.InputSystem;

namespace DM_Customization.Runtime.SimpleShooter
{
    [AddComponentMenu("Game Creator/Custom/Simple Character Shooter")]
    public class SimpleShooterCharacter : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        [SerializeField] private SimpleShooterGun m_SimpleGun;

        // MEMBERS: -----------------------------------------------------------------------
        private Character m_Character;
        private GameObject p_Gun;
        private bool p_IsAiming = false;
        private bool p_IsShooting = false;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void CharacterShoot()
        {
            if (this.p_IsAiming && !this.p_IsShooting)
            {
                p_IsShooting = true;
                this.Shoot(this.m_Character, m_SimpleGun);
            }
        }

        public void CharacterAim()
        {

        }

        public void ChangeWeapon(SimpleShooterGun simpleGun)
        {
            this.UnEquip();
            m_SimpleGun = simpleGun;
            this.Equip(simpleGun);
        }

        public void Equip(SimpleShooterGun simpleGun)
        {
            p_Gun = m_Character.Props.Attach(simpleGun.MainBone, simpleGun.Gun.gameObject, simpleGun.LocationOffset, simpleGun.RotationOffset);
        }

        public void UnEquip()
        {
            m_Character.Props.Remove(m_SimpleGun.Gun.gameObject);
            m_SimpleGun = null;
            p_Gun = null;
        }
        // PRIVATE METHODS: ------------------------------------------------------------------------
        private void Start()
        {

            m_Character = this.GetComponent<Character>();
            if (m_SimpleGun)
                this.Equip(m_SimpleGun);
        }

        private void Shoot(Character character, SimpleShooterGun simpleGun)
        {
            if (p_Gun == null) return;

            Instantiate(simpleGun.Bullet.gameObject, p_Gun.GetComponent<SimpleGunElement>().m_BulletSpawn.transform.position, p_Gun.GetComponent<SimpleGunElement>().m_BulletSpawn.transform.rotation);
            p_IsShooting = false;
        }


        // Debug
        private void Update()
        {
            if(Keyboard.current.anyKey.isPressed)
                Shoot(m_Character, m_SimpleGun);
        }
    }
}