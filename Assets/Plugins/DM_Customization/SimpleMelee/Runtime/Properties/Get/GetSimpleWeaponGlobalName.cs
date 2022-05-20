using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace DM_Customization.Runtime.SimpleMelee
{
    [Title("Global Name Variable")]
    [Category("Variables/Global Name Variable")]
    
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Simple Weapon value of a Global Name Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetSimpleWeaponGlobalName : PropertyTypeGetSimpleWeapon
    {
        [SerializeField]
        protected FieldGetGlobalName m_Variable = new FieldGetGlobalName(SimpleMeleeWeaponValue.TYPE_ID);

        public override SimpleMeleeWeapon Get(Args args) => this.m_Variable.Get<SimpleMeleeWeapon>();
        public override SimpleMeleeWeapon Get(GameObject gameObject) => this.m_Variable.Get<SimpleMeleeWeapon>();

        public override string String => this.m_Variable.ToString();
    }
}