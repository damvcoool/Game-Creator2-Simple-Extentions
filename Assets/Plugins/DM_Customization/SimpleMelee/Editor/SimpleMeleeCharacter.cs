using GameCreator.Editor.Common.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DM_Customization.Runtime;

namespace DM_Customization.Editor
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