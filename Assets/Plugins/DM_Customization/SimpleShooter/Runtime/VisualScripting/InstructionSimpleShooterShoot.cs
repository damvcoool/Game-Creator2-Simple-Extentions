using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.SimpleShooter
{
    [Version(1, 0, 0)]

    [Title("Shoot")]
    [Description("Shoot with current Simple Gun")]

    [Category("Simple Shooter/Shoot")]

    [Parameter("Character", "The Character that will shoot")]

    [Keywords("Stop", "Shooter", "Aim")]

    [Image(typeof(IconBullsEye), ColorTheme.Type.Green)]
    [Serializable]
    public class InstructionSimpleShooterShoot : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        public override string Title => $"{this.m_Character} Shoot Gun";
        protected override Task Run(Args args)
        {
            SimpleShooterCharacter shooterCharacter = this.m_Character.Get(args).GetComponent<SimpleShooterCharacter>();
            if (shooterCharacter == null) return DefaultResult;

            shooterCharacter.CharacterShoot();

            return DefaultResult;
        }
    }
}