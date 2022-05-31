using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
    [CreateAssetMenu(fileName = "Simple Gun", menuName = "Game Creator/Simple Shooter/Simple Gun")]
    public class SimpleShooterGun : ScriptableObject
    {
        [SerializeField] private SimpleGunElements m_GunPrefab;
        [SerializeField] private Vector3 m_Position;
        [SerializeField] private Quaternion m_Rotation;
        [SerializeField] private Bone m_Hand = new Bone(HumanBodyBones.RightHand);
        [SerializeField] private bool m_UseSecondHand = false;
        [SerializeField] private Bone m_SecondHand = new Bone(HumanBodyBones.LeftHand);

        public Bone Bone => m_Hand;
        public GameObject Gun => m_GunPrefab.gameObject;
    }
}