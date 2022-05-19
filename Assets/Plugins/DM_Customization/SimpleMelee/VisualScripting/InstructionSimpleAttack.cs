using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime
{
    [Version(1, 0, 0)]

    [Title("Simple Attack")]
    [Description("Initiates attack with currently equipped weapon")]

    [Category("Simple Melee/Simple Attack")]

    [Parameter("Character", "The Character that will initiate the attack")]

    [Keywords("Attack", "Fight", "Weapon")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Red, typeof(OverlayPlus))]

    [Serializable]
    public class InstructionSimpleAttack : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();

        public override string Title => $"{this.m_Character} perform attack";
        protected override Task Run(Args args)
        {
            GameObject gameObject = this.m_Character.Get(args);

            SimpleMeleeCharacter weapon = gameObject.GetComponent<SimpleMeleeCharacter>();

            weapon.CharacterAttack();

            return DefaultResult;
        }
    }
}