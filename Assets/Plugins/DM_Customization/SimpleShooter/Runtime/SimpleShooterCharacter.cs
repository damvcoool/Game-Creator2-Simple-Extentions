using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Cameras;
using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
    [AddComponentMenu("Game Creator/Custom/Simple Character Shooter")]
    public class SimpleShooterCharacter : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        [SerializeField] private SimpleShooterGun m_SimpleGun;
        [SerializeField] private ShotCamera m_AimCameraShot;

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
                this.Shoot(m_SimpleGun);
            }
        }

        public void CharacterStartAim()
        {
            ConfigState configuration = new ConfigState(
                0, 1, 1,
                0.3f, 0f
            );

            _ = m_Character.States.SetState(
                m_SimpleGun.AimState, m_SimpleGun.Layer,
                m_SimpleGun.BlendMode, configuration
            );
            p_IsAiming = true;
        }
        public void CharacterStoptAim()
        {
            m_Character.States.Stop(m_SimpleGun.Layer, 0f, 0.3f);
            p_IsAiming = false;
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

        private void Shoot(SimpleShooterGun simpleGun)
        {
            if (p_Gun == null) return;

            Instantiate(simpleGun.Bullet.gameObject, p_Gun.GetComponent<SimpleGunElement>().m_BulletSpawn.transform.position, p_Gun.GetComponent<SimpleGunElement>().m_BulletSpawn.transform.rotation);
            p_IsShooting = false;
        }
    }
}