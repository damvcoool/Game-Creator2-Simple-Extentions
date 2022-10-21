using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace DM_Customization.Runtime.SimpleMelee
{
    [Image(typeof(IconWheel), ColorTheme.Type.Green)]
    [Title("Simple Weapon")]
    [Category("Simple Melee/Simple Weapon")]
    public class SimpleMeleeWeaponValue : TValue
    {
        public static readonly IdString TYPE_ID = new IdString("simple-weapon");
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private SimpleMeleeWeapon m_Value;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override IdString TypeID => TYPE_ID;
        public override Type Type => typeof(SimpleMeleeWeapon);

        public override bool CanSave => false;

        public override TValue Copy => new SimpleMeleeWeaponValue
        {
            m_Value = this.m_Value
        };
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public SimpleMeleeWeaponValue() : base()
        { }

        public SimpleMeleeWeaponValue(SimpleMeleeWeapon value) : this()
        {
            this.m_Value = value;
        }

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        protected override object Get()
        {
            return this.m_Value;
        }

        protected override void Set(object value)
        {
            this.m_Value = value is SimpleMeleeWeapon cast ? cast : null;
        }

        public override string ToString()
        {
            return this.m_Value != null ? this.m_Value.name : "(none)";
        }
        // REGISTRATION METHODS: ------------------------------------------------------------------

        [RuntimeInitializeOnLoadMethod]
        private static void RuntimeInit() => RegisterValueType(
            TYPE_ID,
            new TypeData(typeof(SimpleMeleeWeaponValue), CreateValue)
        );

#if UNITY_EDITOR

        [UnityEditor.InitializeOnLoadMethod]
        private static void EditorInit() => RegisterValueType(
            TYPE_ID,
            new TypeData(typeof(SimpleMeleeWeaponValue), CreateValue)
        );

#endif

        private static SimpleMeleeWeaponValue CreateValue(object value)
        {
            return new SimpleMeleeWeaponValue(value as SimpleMeleeWeapon);
        }
    }
}