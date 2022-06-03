using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{

    [CreateAssetMenu(fileName = "Simple Gun", menuName = "Game Creator/Simple Shooter/Simple Gun")]
    public class SimpleShooterGun : ScriptableObject
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private SimpleGunElements m_GunPrefab;
        [SerializeField] private SimpleShooterBullet m_BulletPrefab;
        [SerializeField] private State m_AimState;
        [SerializeField] private int m_Layer = 1;
        [SerializeField] private BlendMode m_BlendMode = BlendMode.Blend;
        [SerializeField] private Vector3 m_Position;
        [SerializeField] private Vector3 m_Rotation;
        [SerializeField] private Bone m_MainHand = new Bone(HumanBodyBones.RightHand);
        [SerializeField] private bool m_UseSecondHand = false;
        [SerializeField] private Bone m_SecondHand = new Bone(HumanBodyBones.LeftHand);

        // PROPERTIES: ----------------------------------------------------------------------------

        public Bone Bone => m_MainHand;
        public GameObject GunPrefab => m_GunPrefab.gameObject;
        public Vector3 LocationOffset => this.m_Position;
        public Quaternion RotationOffset => Quaternion.Euler(this.m_Rotation);
    }
}