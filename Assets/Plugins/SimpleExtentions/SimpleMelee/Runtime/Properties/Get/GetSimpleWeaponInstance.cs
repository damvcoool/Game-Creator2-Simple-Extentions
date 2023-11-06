using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleMelee
{
    [Title("Simple Weapon")]
    [Category("Simple Weapon")]
    
    [Image(typeof(IconAnimator), ColorTheme.Type.Purple)]
    [Description("A reference to an Simple Weapon asset")]

    [Serializable] [HideLabelsInEditor]
    public class GetSimpleWeaponInstance : PropertyTypeGetSimpleWeapon
    {
        [SerializeField] protected SimpleMeleeWeapon m_SimpleWeapon;

        public override SimpleMeleeWeapon Get(Args args) => this.m_SimpleWeapon;
        public override SimpleMeleeWeapon Get(GameObject gameObject) => this.m_SimpleWeapon;

        public static PropertyGetSimpleWeapon Create(SimpleMeleeWeapon simpleWeapon = null)
        {
            GetSimpleWeaponInstance instance = new GetSimpleWeaponInstance
            {
                m_SimpleWeapon = simpleWeapon
            };
            
            return new PropertyGetSimpleWeapon(instance);
        }

        public override string String => this.m_SimpleWeapon != null
            ? this.m_SimpleWeapon.name
            : "(none)";
    }
}