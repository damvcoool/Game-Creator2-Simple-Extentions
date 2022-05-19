using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime 
{
    [Version(1, 0, 0)]

    [Title("Change Weapon")]
    [Description("Changes equipped weapon")]

    [Category("Simple Melee/Change Weapon")]

    [Parameter("Character", "The Character that will initiate the attack")]
    [Parameter("Melee Weapon", "The new Weapon")]

    [Keywords("Attack", "Fight", "Weapon")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Red, typeof(OverlayPlus))]

    [Serializable]
    public class InstructionSimpleMeleeEquip : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private SimpleMeleeWeapon m_MeleeWeapon;

        public override string Title => $"Change {this.m_Character} Weapon for {m_MeleeWeapon.name}";
        protected override Task Run(Args args)
        {
            if (m_MeleeWeapon == null) return DefaultResult;

            SimpleMeleeCharacter characterMelee = m_Character.Get(args).GetComponent<SimpleMeleeCharacter>();
            if (characterMelee == null) return DefaultResult;

            characterMelee.UnEquip();
            characterMelee.m_Weapon = m_MeleeWeapon;
            characterMelee.Equip();

            return DefaultResult;
        }
    }
}