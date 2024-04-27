using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Version(1, 0, 0)]

    [Title("Compare Equiped Weapon")]
    [Description("Compares Currenly Equiped with other Weapon")]

    [Category("Simple Melee/Compare Simple Melee Weapon")]

    [Parameter("Character", "Character with weapon equiped")]
    [Parameter("SimpleWeapon", "Weapon to Compare")]

    [Keywords("Attack", "Fight", "Weapon")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Red, typeof(OverlayPlus))]


    [Serializable]
    public class ConditionSimpleMeleeCompareWeapon : Condition
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private SimpleMeleeWeapon m_MeleeWeapon;

        protected override string Summary => $"{this.m_Character} has {this.m_MeleeWeapon.name} equiped";
        protected override bool Run(Args args)
        {
            SimpleMeleeCharacter characterMelee = m_Character.Get(args).GetComponent<SimpleMeleeCharacter>();

            if (characterMelee.m_Weapon == m_MeleeWeapon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}