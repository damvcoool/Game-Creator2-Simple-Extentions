using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Title("Local Name Variable")]
    [Category("Local Name Variable")]
    
    [Description("Sets the Simple Weapon value on a Local Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]

    [Serializable] [HideLabelsInEditor]
    public class SetSimpleWeaponLocalName : PropertyTypeSetSimpleWeapon
    {
        [SerializeField]
        protected FieldSetLocalName m_Variable = new FieldSetLocalName(SimpleMeleeWeaponValue.TYPE_ID);

        public override void Set(SimpleMeleeWeapon value, Args args) => this.m_Variable.Set(value, args);
        //public override void Set(SimpleMeleeWeapon value, GameObject gameObject) => this.m_Variable.Set(value, gameObject);

        public override SimpleMeleeWeapon Get(Args args) => this.m_Variable.Get(args) as SimpleMeleeWeapon;
        //public override SimpleMeleeWeapon Get(GameObject gameObject) => this.m_Variable.Get() as SimpleMeleeWeapon;
        
        public static PropertySetSimpleWeapon Create => new PropertySetSimpleWeapon(
            new SetSimpleWeaponLocalName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}