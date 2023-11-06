using System;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Serializable]
    public class PropertyGetSimpleWeapon : TPropertyGet<PropertyTypeGetSimpleWeapon, SimpleMeleeWeapon>
    {
        public PropertyGetSimpleWeapon() : base(new GetSimpleWeaponInstance())
        { }

        public PropertyGetSimpleWeapon(PropertyTypeGetSimpleWeapon defaultType) : base(defaultType)
        { }
    }
}