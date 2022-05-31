using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DM_Customization.Runtime.SimpleShooter
{
    [CreateAssetMenu(fileName = "Simple Bullet", menuName = "Game Creator/Simple Shooter/Simple Bullet")]
    [RequireComponent(typeof(Collider))]
    public class SimpleShooterBullet : MonoBehaviour
    {
        [SerializeField] private float m_ShootSpeed;
        private Rigidbody rb;

        private void Awake()
        {
            if (!GetComponent<Rigidbody>())
                gameObject.AddComponent<SimpleShooterBullet>();

            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            
        }
        private void Start()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }

    }
}