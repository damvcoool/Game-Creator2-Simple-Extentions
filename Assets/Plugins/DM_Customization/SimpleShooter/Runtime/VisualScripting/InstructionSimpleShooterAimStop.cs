using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.SimpleShooter
{
    [Version(1, 0, 0)]

    [Title("Stop Aiming")]
    [Description("Stop Aiming State")]

    [Category("Simple Shooter/Stop Aiming")]

    [Parameter("Character", "The Character that will stop aiming")]

    [Keywords("Stop", "Shooter", "Aim")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Red, typeof(OverlayPlus))]

    [Serializable]
    public class InstructionSimpleShooterAimStop : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();

        public override string Title => $"{this.m_Character} Stop Aiming";
        protected override Task Run(Args args)
        {
            SimpleShooterCharacter shooterCharacter = this.m_Character.Get(args).GetComponent<SimpleShooterCharacter>();
            if (shooterCharacter == null) return DefaultResult;

            shooterCharacter.CharacterStoptAim();

            return DefaultResult;
        }
    }
}