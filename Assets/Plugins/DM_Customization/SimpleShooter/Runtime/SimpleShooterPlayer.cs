using System.Collections;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;

namespace DM_Customization.Runtime.SimpleShooter
{
    public class SimpleShooterPlayer : MonoBehaviour
    {
        [SerializeField] private SimpleShooterGun m_SimpleGun;

        private Character m_Character;
        private GameObject p_Weapon = null;

        private void Start()
        {

            m_Character = this.GetComponent<Character>();
            if (m_SimpleGun)
                this.Equip();
        }
         

        private void Equip()
        {
            p_Weapon = m_Character.Props.Attach(m_SimpleGun.Bone, m_SimpleGun.GunPrefab, m_SimpleGun.LocationOffset, m_SimpleGun.RotationOffset);
        }


    }
}