using System;
using GameCreator.Runtime.Common;

namespace DM_Customization.Runtime.SimpleMelee
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