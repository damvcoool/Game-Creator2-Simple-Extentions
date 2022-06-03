using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
    [AddComponentMenu("Game Creator/Custom/Simple")]
    public class SimpleGunElements : MonoBehaviour
    {
        public Transform m_BulletSpawn;
        public Transform m_SecondHandPosition;

        public void ShootInstantiate(SimpleShooterBullet Bullet)
        {
            Instantiate(Bullet.gameObject, m_BulletSpawn.position, m_BulletSpawn.rotation);
        }
    }
}