using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace DM_Customization.Runtime.SimpleMelee
{
    [Title("Global Name Variable")]
    [Category("Global Name Variable")]
    
    [Description("Sets the Simple Weapon value on a Global Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable] [HideLabelsInEditor]
    public class SetSimpleWeaponGlobalName : PropertyTypeSetSimpleWeapon
    {
        [SerializeField]
        protected FieldSetGlobalName m_Variable = new FieldSetGlobalName(SimpleMeleeWeaponValue.TYPE_ID);

        public override void Set(SimpleMeleeWeapon value, Args args) => this.m_Variable.Set(value, args);
        //public override void Set(SimpleMeleeWeapon value, GameObject gameObject) => this.m_Variable.Set(value);

        public override SimpleMeleeWeapon Get(Args args) => this.m_Variable.Get(args) as SimpleMeleeWeapon;
        //public override SimpleMeleeWeapon Get(GameObject gameObject) => this.m_Variable.Get() as SimpleMeleeWeapon;
        
        public static PropertySetSimpleWeapon Create => new PropertySetSimpleWeapon(
            new SetSimpleWeaponGlobalName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}