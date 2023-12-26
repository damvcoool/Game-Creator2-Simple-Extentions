using GameCreator.Editor.Common.UnityUI;
using GameCreator.Editor.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using SimpleExtentions.Runtime.SimpleDialogue;
using TMPro;
using Image = UnityEngine.UI.Image;

namespace SimpleExtentions.Editor.SimpleDialogue
{
    [CustomEditor(typeof(SimpleDialogueUI))]
    public class SimpleDialogueUIEditor : UnityEditor.Editor
    {
        // MEMBERS: -------------------------------------------------------------------------------
        private VisualElement m_Root;
        private VisualElement m_Head;
        private VisualElement m_Body;
        // PAINT METHOD: --------------------------------------------------------------------------
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();
            this.m_Head = new VisualElement();
            this.m_Body = new VisualElement();

            this.m_Root.Add(m_Head);
            this.m_Root.Add(m_Body);

            SerializedProperty diaglogueText = this.serializedObject.FindProperty("m_DialogueText");
            SerializedProperty intervalTime = this.serializedObject.FindProperty("m_IntervalTime");
            SerializedProperty container = this.serializedObject.FindProperty("m_Container");
            SerializedProperty text = this.serializedObject.FindProperty("m_Text");
            SerializedProperty speaker = this.serializedObject.FindProperty("m_CurrentSpeaker");
            SerializedProperty useBillboard = this.serializedObject.FindProperty("m_UseBillboard");
            SerializedProperty updateTime = this.serializedObject.FindProperty("m_UpdateTime");

            PropertyField fieldDiaglogueText = new PropertyField(diaglogueText);
            PropertyField fieldIntervalTime = new PropertyField(intervalTime);
            PropertyField fieldUpdateTime = new PropertyField(updateTime);
            PropertyField fieldContainer = new PropertyField(container);
            PropertyField fieldText = new PropertyField(text);
            PropertyField fieldSpeaker = new PropertyField(speaker);
            PropertyField fieldUseBillboard = new PropertyField(useBillboard);

            this.m_Head.Add(fieldUpdateTime);
            this.m_Head.Add(fieldDiaglogueText);
            this.m_Body.Add(fieldIntervalTime);
            this.m_Body.Add(fieldContainer);
            this.m_Body.Add(fieldText);
            this.m_Body.Add(fieldSpeaker);
            this.m_Body.Add(fieldUseBillboard);
                        
            return this.m_Root;
        }
        // CREATION MENU: -------------------------------------------------------------------------

        [MenuItem("GameObject/Game Creator/UI/Simple Dialogue UI (TMP)", false, 0)]
        public static void CreateElementTMP(MenuCommand menuCommand)
        {
            TMP_DefaultControls.Resources TMPResources = new TMP_DefaultControls.Resources();

            GameObject canvas = UnityUIUtilities.GetCanvas();
            GameObject simpleDialogueUI = new GameObject("Simple Dialogue UI",typeof(RectTransform));

            simpleDialogueUI.transform.SetParent(canvas.transform, false);

            DefaultControls.Resources resources = UnityUIUtilities.GetStandardResources();

            GameObject Container = DefaultControls.CreateImage(resources);
            Container.transform.SetParent(simpleDialogueUI.transform, false);
            Container.name = "Container";

            
            GameObject Text = TMP_DefaultControls.CreateText(TMPResources);
            Text.transform.SetParent(Container.transform, false);
            Text.name = "Text";

            TMP_Text text = Text.GetComponent<TMP_Text>();
            text.SetText("Hello World");
            text.color = Color.black;

            SimpleDialogueUI.CreateFromTMP(simpleDialogueUI, Container, text);

            Undo.RegisterCreatedObjectUndo(simpleDialogueUI, $"Create {simpleDialogueUI.gameObject.name}");
            Selection.activeGameObject = simpleDialogueUI;
        }

        [MenuItem("GameObject/Game Creator/UI/Simple Dialogue UI", false, 0)]
        public static void CreateElement(MenuCommand menuCommand)
        {
            GameObject canvas = UnityUIUtilities.GetCanvas();
            GameObject simpleDialogueUI = new GameObject("Simple Dialogue UI", typeof(RectTransform));

            simpleDialogueUI.transform.SetParent(canvas.transform, false);

            DefaultControls.Resources resources = UnityUIUtilities.GetStandardResources();

            GameObject Container = DefaultControls.CreateImage(resources);
            Container.transform.SetParent(simpleDialogueUI.transform, false);
            Container.name = "Container";

            GameObject Text = DefaultControls.CreateText(resources);
            Text.transform.SetParent(Container.transform, false);
            Text.name = "Text";

            Text text = Text.GetComponent<Text>();
            text.text = "Hello World";

            SimpleDialogueUI.CreateFrom(simpleDialogueUI, Container, text);

            Undo.RegisterCreatedObjectUndo(simpleDialogueUI, $"Create {simpleDialogueUI.gameObject.name}");
            Selection.activeGameObject = simpleDialogueUI;
        }
    }
}