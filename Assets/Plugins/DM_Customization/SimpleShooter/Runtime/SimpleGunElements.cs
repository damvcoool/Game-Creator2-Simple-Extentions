using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
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