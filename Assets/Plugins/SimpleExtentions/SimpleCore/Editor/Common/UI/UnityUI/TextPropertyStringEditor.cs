using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using GameCreator.Editor.Common.UnityUI;
using SimpleExtentions.Runtime.Common.UnityUI;

namespace SimpleExtentions.Editor.Common.UnityUI
{
    [CustomEditor(typeof(TextPropertyString))]
    public class TextPropertyStringEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            VisualElement head = new VisualElement();
            VisualElement body = new VisualElement();

            root.Add(head);
            root.Add(body);
            
            //SerializedProperty textUI = this.serializedObject.FindProperty("m_textUI");
            SerializedProperty propertyString = this.serializedObject.FindProperty("m_PropertyString");

            //PropertyTool fieldTextUI = new PropertyTool(textUI);
            PropertyField fieldPropertyString = new PropertyField(propertyString);

            //body.Add(fieldTextUI);
            body.Add(fieldPropertyString);

            return root;
        }
        // CREATE: --------------------------------------------------------------------------------

        [MenuItem("GameObject/Game Creator/UI/Text UI", false, 0)]
        public static void CreateElementTMP()
        {
            GameObject canvas = UnityUIUtilities.GetCanvas();
            GameObject gameObject = new GameObject();

            gameObject.transform.SetParent(canvas.transform, false);
            gameObject.name = "Text UI";

            TextPropertyString.CreateFromTMP(gameObject);

            Undo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");
            Selection.activeGameObject = gameObject;
        }
        
        [MenuItem("GameObject/Game Creator/UI/Text UI (Legacy)", false, 0)]
        public static void CreateElemenLegacy()
        {

            GameObject canvas = UnityUIUtilities.GetCanvas();
            GameObject gameObject = new GameObject();

            gameObject.transform.SetParent(canvas.transform, false);
            gameObject.name = "Text UI (Legacy)";

            TextPropertyString.CreateFromLegacy(gameObject);

            Undo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");
            Selection.activeGameObject = gameObject;
        }
    }
}
