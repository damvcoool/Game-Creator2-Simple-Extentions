using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;


namespace DM_Customization.Runtime.SimpleShooter
{
    [AddComponentMenu("Game Creator/Custom/Simple Bullet")]
    [RequireComponent(typeof(Collider))]
    public class SimpleShooterBullet : MonoBehaviour
    {

        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        [SerializeField] private float m_BulletSpeed = 20;

        [SerializeField] private InstructionList m_OnBulletHit = new InstructionList(new InstructionCommonDebugText("Boom!"));
        [SerializeField] private bool m_DestroyOnImpact = true;

        // MEMBERS: -----------------------------------------------------------------------
        private Rigidbody rb;
        private GameObject target;
        private bool isTrigger = false;

        // EVENTS: --------------------------------------------------------------------------------

        public static event Action EventOnBulletHit;

        // INITIALIZERS: --------------------------------------------------------------------------
        private void OnEnable() => EventOnBulletHit += this.OnBulletHit;

        private void OnDisable() => EventOnBulletHit -= this.OnBulletHit;

        // PRIVATE METHODS: ------------------------------------------------------------------------

        private async void OnBulletHit()
        {
            Args args = new Args(target);
            await this.m_OnBulletHit.Run(args);
            if (m_DestroyOnImpact)
                Destroy(this.gameObject);
        }

        private void Awake()
        {

            if (this.GetComponent<Collider>().isTrigger) isTrigger = true;

            if (!GetComponent<Rigidbody>())
                gameObject.AddComponent<Rigidbody>();
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        private void Start()
        {
            rb.velocity = transform.forward * m_BulletSpeed;

            Destroy(this.gameObject, 30);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isTrigger)
            {
                target = other.gameObject;
                OnBulletHit();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!isTrigger)
            {
                target = collision.gameObject;
                OnBulletHit();
            }
        }
    }
}