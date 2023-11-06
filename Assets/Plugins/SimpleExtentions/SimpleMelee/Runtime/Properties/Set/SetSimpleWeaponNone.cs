using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Title("None")]
    [Category("None")]
    [Description("Don't save on anything")]
    
    [Image(typeof(IconNull), ColorTheme.Type.TextLight)]

    [Serializable]
    public class SetSimpleWeaponNone : PropertyTypeSetSimpleWeapon
    {
        public override void Set(SimpleMeleeWeapon value, Args args)
        { }
        
        public override void Set(SimpleMeleeWeapon value, GameObject gameObject)
        { }

        public static PropertySetSimpleWeapon Create => new PropertySetSimpleWeapon(
            new SetSimpleWeaponNone()
        );

        public override string String => "(none)";
    }
}