using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Title("Local Name Variable")]
    [Category("Variables/Local Name Variable")]

    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]
    [Description("Returns the SimpleMeleeWeapon value of a Local Name Variable")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetSimpleWeaponLocalName : PropertyTypeGetSimpleWeapon
    {
        [SerializeField]
        protected FieldGetLocalName m_Variable = new FieldGetLocalName(SimpleMeleeWeaponValue.TYPE_ID);

        public override SimpleMeleeWeapon Get(Args args) => this.m_Variable.Get<SimpleMeleeWeapon>(args);
        //public override SimpleMeleeWeapon Get(GameObject gameObject) => this.m_Variable.Get<SimpleMeleeWeapon>();

        public override string String => this.m_Variable.ToString();
    }
}