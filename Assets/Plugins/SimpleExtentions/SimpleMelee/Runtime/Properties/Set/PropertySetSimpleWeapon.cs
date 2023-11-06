using System;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Serializable]
    public class PropertySetSimpleWeapon : TPropertySet<PropertyTypeSetSimpleWeapon, SimpleMeleeWeapon>
    {
        public PropertySetSimpleWeapon() : base(new SetSimpleWeaponNone())
        { }

        public PropertySetSimpleWeapon(PropertyTypeSetSimpleWeapon defaultType) : base(defaultType)
        { }
    }
}