using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Title("Global List Variable")]
    [Category("Variables/Global List Variable")]
    
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal, typeof(OverlayDot))]
    [Description("Returns the Simple Weapon value of a Global List Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetSimpleWeaponGlobalList : PropertyTypeGetSimpleWeapon
    {
        [SerializeField]
        protected FieldGetGlobalList m_Variable = new FieldGetGlobalList(SimpleMeleeWeaponValue.TYPE_ID);

        public override SimpleMeleeWeapon Get(Args args) => this.m_Variable.Get<SimpleMeleeWeapon>(args);
        //public override SimpleMeleeWeapon Get(GameObject gameObject) => this.m_Variable.Get<SimpleMeleeWeapon>();

        public override string String => this.m_Variable.ToString();
    }
}