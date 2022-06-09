using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
    [AddComponentMenu("Game Creator/Custom/Simple Gun")]
    public class SimpleGunElement : MonoBehaviour
    {
        public Transform m_BulletSpawn;
        public Transform m_SecondHandPosition;
    }
}