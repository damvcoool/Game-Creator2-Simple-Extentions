using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Stats;
using Attribute = GameCreator.Runtime.Stats.Attribute;

namespace DM_Customization.Runtime
{
    [Version(1, 0, 0)]

    [Title("Simple Attack with Stamina")]
    [Description("Initiates attack with currently equipped weapon")]

    [Category("Simple Melee/Simple Attac kwith Stamina")]

    [Parameter("Character", "The Character that will initiate the attack")]
    [Parameter("Stamina", "The Attribute type that changes its value")]
    [Parameter("Change", "The value changed")]

    [Keywords("Attack", "Fight", "Weapon")]

    [Image(typeof(IconCharacterGesture), ColorTheme.Type.Red, typeof(OverlayPlus))]

    [Serializable]
    public class InstructionSimpleAttackUsingStamina : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private Attribute m_Stamina = new Attribute();
        [SerializeField] private ChangeDecimal m_Change = new ChangeDecimal();

        public override string Title => $"{this.m_Character} perform attack and use {m_Change} of Stamina";
        protected override Task Run(Args args)
        {
            GameObject gameObject = this.m_Character.Get(args);
            if (gameObject == null) return DefaultResult;

            Traits traits = gameObject.Get<Traits>();
            if (traits == null) return DefaultResult;

            if (this.m_Stamina == null) return DefaultResult;
            RuntimeAttributeData attribute = traits.RuntimeAttributes.Get(this.m_Stamina.ID);
            if (attribute == null) return DefaultResult;

            SimpleCharacterMelee weapon = gameObject.GetComponent<SimpleCharacterMelee>();
            if (weapon == null) return DefaultResult;

            if (!weapon.IsAttacking())
            {
                attribute.Value = (float)this.m_Change.Get(attribute.Value, args);
                weapon.CharacterAttack();
            }

            return DefaultResult;
        }
    }
}