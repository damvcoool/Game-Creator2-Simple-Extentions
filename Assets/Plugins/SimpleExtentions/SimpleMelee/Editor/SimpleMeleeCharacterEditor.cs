using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using SimpleExtentions.Runtime.SimpleMelee;

namespace SimpleExtentions.Editor.SimpleMelee
{
    [CustomEditor(typeof(SimpleMeleeCharacter))]
    public class SimpleMeleeCharacterEditor : UnityEditor.Editor
    {
        // MEMBERS: -------------------------------------------------------------------------------
        private VisualElement m_Root;
        // PAINT METHOD: --------------------------------------------------------------------------
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty weapon = this.serializedObject.FindProperty("m_Weapon");

            PropertyField fieldWeapon = new PropertyField(weapon);

            this.m_Root.Add(fieldWeapon);

            return this.m_Root;
        }
    }
}