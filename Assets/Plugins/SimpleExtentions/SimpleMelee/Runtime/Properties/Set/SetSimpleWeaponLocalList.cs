using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Title("Local List Variable")]
    [Category("Local List Variable")]
    
    [Description("Sets the Simple Weapon value on a Local List Variable")]
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal)]

    [Serializable] [HideLabelsInEditor]
    public class SetSimpleWeaponLocalList : PropertyTypeSetSimpleWeapon
    {
        [SerializeField]
        protected FieldSetLocalList m_Variable = new FieldSetLocalList(SimpleMeleeWeaponValue.TYPE_ID);

        public override void Set(SimpleMeleeWeapon value, Args args) => this.m_Variable.Set(value, args);

        public override SimpleMeleeWeapon Get(Args args) => this.m_Variable.Get(args) as SimpleMeleeWeapon;
        
        public static PropertySetSimpleWeapon Create => new PropertySetSimpleWeapon(
            new SetSimpleWeaponLocalList()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}