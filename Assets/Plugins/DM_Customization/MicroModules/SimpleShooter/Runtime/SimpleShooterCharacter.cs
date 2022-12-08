using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Characters.IK;
using UnityEngine.Animations.Rigging;
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
        [SerializeField] private LayerMask m_AimColliderLayerMask = new LayerMask();
        [SerializeField] private bool m_DisableJumpWhileAiming = true;

        // MEMBERS: -----------------------------------------------------------------------
        private Character m_Character;
        private GameObject p_Gun;
        private bool p_IsAiming = false;
        private bool p_IsShooting = false;
        private Transform m_AimPosition;
        private TCamera m_Camera;
        private ShotCamera m_OriginalShotCamera;
        private Rig m_ArmRig;

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
            
            m_Camera.Transition.ChangeToShot(m_AimCameraShot, 0.3f, Easing.Type.Linear);
            if (!m_Character.IK.HasRig<AimTrack>()) { Debug.LogWarning($"{m_Character.name} is missing Aim Weapon IK Track"); return; }
            m_Character.IK.GetRig<AimTrack>().SetTarget(new LookTrackTransform(1, m_AimPosition, Vector3.zero));

            if (m_Character.IK.HasRig<RigLookTrack>())
                m_Character.IK.GetRig<RigLookTrack>().SetTarget(new LookTrackTransform(1, m_AimPosition, Vector3.zero));
            m_Character.IK.GetRig<RigLookTrack>().IsActive = false;

            if (m_Character.IK.HasRig<RigLean>())
                m_Character.IK.GetRig<RigLean>().IsActive = false;

            if (m_DisableJumpWhileAiming) m_Character.Motion.CanJump = false;
            p_IsAiming = true;
        }
        public void CharacterStoptAim()
        {
            m_Character.States.Stop(m_SimpleGun.Layer, 0f, 0.3f);
            
            m_Camera.Transition.ChangeToShot(m_OriginalShotCamera, 0.3f, Easing.Type.Linear);
            
            m_Character.IK.GetRig<AimTrack>().RemoveTarget(new LookTrackTransform(1, m_AimPosition, Vector3.zero));

            if (m_Character.IK.HasRig<RigLookTrack>())
                m_Character.IK.GetRig<RigLookTrack>().IsActive = true;// RemoveTarget(new LookTrackTransform(1, m_AimPosition, Vector3.zero));

            if (m_Character.IK.HasRig<RigLean>())
                m_Character.IK.GetRig<RigLean>().IsActive = true;

            if (m_DisableJumpWhileAiming) m_Character.Motion.CanJump = true;
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
            p_Gun = m_Character.Props.AttachPrefab(simpleGun.MainBone, simpleGun.Gun.gameObject, simpleGun.LocationOffset, simpleGun.RotationOffset);
        }

        public void UnEquip()
        {
            m_Character.Props.RemovePrefab(m_SimpleGun.Gun.gameObject);
            m_SimpleGun = null;
            p_Gun = null;
        }
        // PRIVATE METHODS: ------------------------------------------------------------------------
        private void Start()
        {

            m_Character = this.GetComponent<Character>();
            if (m_SimpleGun)
                this.Equip(m_SimpleGun);

            GameObject aim = new GameObject();
            m_AimPosition = aim.transform;

            m_Camera = Camera.main.GetComponent<TCamera>();
            // NOTE: Not sure if this should use Shortcut to Main shot or get the current Shot directlyu from the main camera.
            m_OriginalShotCamera = ShortcutMainShot.Instance.gameObject.GetComponent<ShotCamera>();
            //m_OriginalShotCamera = m_Camera.Transition.CurrentShotCamera;
            

            
        }

        private void Shoot(SimpleShooterGun simpleGun)
        {
            if (p_Gun == null) return;

            Instantiate(simpleGun.Bullet.gameObject, p_Gun.GetComponent<SimpleGunElement>().m_BulletSpawn.transform.position, p_Gun.GetComponent<SimpleGunElement>().m_BulletSpawn.transform.rotation);
            p_IsShooting = false;
        }

        private void Update()
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);

            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            Transform hitTransform = null;

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, m_AimColliderLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
                m_AimPosition.position = raycastHit.point;
                hitTransform = raycastHit.transform;
            }
        }
    }
}