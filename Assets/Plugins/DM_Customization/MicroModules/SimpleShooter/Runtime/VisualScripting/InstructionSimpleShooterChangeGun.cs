using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.SimpleShooter
{
    [Version(1, 0, 0)]

    [Title("Change Gun")]
    [Description("Change the current gun")]

    [Category("Simple Shooter/Change Gun")]

    [Parameter("Character", "The Character that will shoot")]

    [Keywords("Stop", "Shooter", "Aim")]

    [Image(typeof(IconBoltSolid), ColorTheme.Type.Green)]

    [Serializable]
    public class InstructionSimpleShooterChangeGun : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private SimpleShooterGun m_NewGun;
        public override string Title => $"{this.m_Character} change Gun to {m_NewGun}";
        protected override Task Run(Args args)
        {
            SimpleShooterCharacter shooterCharacter = this.m_Character.Get(args).GetComponent<SimpleShooterCharacter>();
            if (shooterCharacter == null) return DefaultResult;
            
            if(m_NewGun == null) return DefaultResult;

            shooterCharacter.ChangeWeapon(m_NewGun);

            return DefaultResult;
        }
    }
}