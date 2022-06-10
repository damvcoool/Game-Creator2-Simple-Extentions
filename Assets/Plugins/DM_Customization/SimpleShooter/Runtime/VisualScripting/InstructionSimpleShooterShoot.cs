using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.SimpleShooter
{
    [Serializable]
    public class InstructionSimpleShooterShoot : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        protected override Task Run(Args args)
        {
            SimpleShooterCharacter shooterCharacter = this.m_Character.Get(args).GetComponent<SimpleShooterCharacter>();
            if (shooterCharacter == null) return DefaultResult;

            shooterCharacter.CharacterShoot();

            return DefaultResult;
        }
    }
}