using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.SimpleShooter
{
    [Version(1, 0, 0)]

    [Title("Remove Gun")]
    [Description("Unequip current gun")]

    [Category("Simple Shooter/Remove Gun")]

    [Parameter("Character", "The Character that will shoot")]

    [Keywords("Stop", "Shooter", "Aim")]

    [Image(typeof(IconBoneOutline), ColorTheme.Type.Red)]

    [Serializable]
    public class InstructionSimpleShooterUnequipGun : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        public override string Title => $"{this.m_Character} Unequip Gun";
        protected override Task Run(Args args)
        {
            SimpleShooterCharacter shooterCharacter = this.m_Character.Get(args).GetComponent<SimpleShooterCharacter>();
            if (shooterCharacter == null) return DefaultResult;

            shooterCharacter.UnEquip();

            return DefaultResult;
        }
    }
}