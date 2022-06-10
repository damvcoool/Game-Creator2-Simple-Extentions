using System.Collections;
using System.Collections.Generic;
using System;
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
            if (!GetComponent<Rigidbody>())
                gameObject.AddComponent<Rigidbody>();

            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            
        }
        private void Start()
        {
            rb.velocity = transform.forward * m_BulletSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            target = other.gameObject;
            OnBulletHit();
        }
    }
}