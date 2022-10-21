using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.SimpleShooter
{
    [Version(1, 0, 0)]

    [Title("Start Aiming")]
    [Description("Starts Aiming State")]

    [Category("Simple Shooter/Start Aiming")]

    [Parameter("Character", "The Character that will start aiming")]

    [Keywords("Start", "Shooter", "Aim")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Green, typeof(OverlayPlus))]

    [Serializable]
    public class InstructionSimpleShooterAimStart : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();

        public override string Title => $"{this.m_Character} Start Aiming";
        protected override Task Run(Args args)
        {
            SimpleShooterCharacter shooterCharacter = this.m_Character.Get(args).GetComponent<SimpleShooterCharacter>();
            if (shooterCharacter == null) return DefaultResult;

            shooterCharacter.CharacterStartAim();

            return DefaultResult;
        }
    }
}