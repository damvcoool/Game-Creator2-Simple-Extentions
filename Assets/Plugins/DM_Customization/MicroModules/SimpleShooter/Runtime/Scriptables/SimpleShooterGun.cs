using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{

    [CreateAssetMenu(fileName = "Simple Gun", menuName = "Game Creator/Simple Shooter/Simple Gun")]
    public class SimpleShooterGun : ScriptableObject
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private SimpleGunElement m_GunPrefab;
        [SerializeField] private SimpleShooterBullet m_BulletPrefab;
        [SerializeField] private State m_AimState;
        [SerializeField] private int m_Layer = 1;
        [SerializeField] private BlendMode m_BlendMode = BlendMode.Blend;
        [SerializeField] private Bone m_MainHand = new Bone(HumanBodyBones.RightHand);
        [SerializeField] private Vector3 m_Position;
        [SerializeField] private Vector3 m_Rotation;
        [SerializeField] private bool m_UseSecondHand = false;
        [SerializeField] private Bone m_SecondHand = new Bone(HumanBodyBones.LeftHand);

        // PROPERTIES: ----------------------------------------------------------------------------

        public SimpleGunElement Gun => m_GunPrefab;
        public SimpleShooterBullet Bullet => m_BulletPrefab;
        public State AimState => m_AimState;
        public int Layer => m_Layer;
        public BlendMode BlendMode => m_BlendMode;
        public Bone MainBone => m_MainHand;
        public Vector3 LocationOffset => this.m_Position;
        public Quaternion RotationOffset => Quaternion.Euler(this.m_Rotation);
        public bool UseSecondHand => this.m_UseSecondHand;
        public Bone SecondBone => m_SecondHand;
    }
}