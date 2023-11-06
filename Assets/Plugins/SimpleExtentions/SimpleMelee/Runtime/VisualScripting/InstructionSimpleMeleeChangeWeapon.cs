using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Characters;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Version(1, 0, 0)]

    [Title("Change Melee Weapon")]
    [Description("Changes equipped melee weapon")]

    [Category("Simple Melee/Change Melee Weapon")]

    [Parameter("Character", "The Character that will initiate the attack")]
    [Parameter("Melee Weapon", "The new Weapon")]

    [Keywords("Attack", "Fight", "Weapon")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Red, typeof(OverlayPlus))]

    [Serializable]
    public class InstructionSimpleMeleeEquip : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private SimpleMeleeWeapon m_MeleeWeapon;

        public override string Title => string.Format("Change {0} Weapon for {1}", this.m_Character, m_MeleeWeapon ? m_MeleeWeapon.name : "Unequip");
        protected override Task Run(Args args)
        {
            //if (m_MeleeWeapon == null) return DefaultResult;

            SimpleMeleeCharacter characterMelee = m_Character.Get(args).GetComponent<SimpleMeleeCharacter>();
            if (characterMelee == null) return DefaultResult;

            if(characterMelee.HasWeapon()) characterMelee.UnEquip();
            if (m_MeleeWeapon != null)
            {
                characterMelee.m_Weapon = m_MeleeWeapon;
                characterMelee.Equip();
            }
            Debug.Log(m_MeleeWeapon);

            return DefaultResult;
        }
    }
}