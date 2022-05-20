using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Gun", menuName = "Game Creator/Simple Shooter/Simple Gun")]
public class SimpleShooterGun : ScriptableObject
{
    public static SimpleShooterGun instance;

    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private List<SimpleShooterBullet> m_SimpleBullets;
    [SerializeField] private Vector3 m_Position;
    [SerializeField] private Quaternion m_Rotation;
    
}
