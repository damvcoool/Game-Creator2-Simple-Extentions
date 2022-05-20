using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Simple Bullet", menuName ="Game Creator/Simple Shooter/Simple Bullet")]
public class SimpleShooterBullet : ScriptableObject
{
    public static SimpleShooterBullet instance;

    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private float m_ReloadSpeed;
    [SerializeField] private float m_ShootSpeed;

    private void Awake()
    {
        if (!m_BulletPrefab)
            Debug.LogWarning("No bullet options here");

    }
}
