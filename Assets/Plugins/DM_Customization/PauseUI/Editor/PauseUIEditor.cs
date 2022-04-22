using GameCreator.Editor.Common.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DM_Customization.Editor
{
    [CustomEditor(typeof(PauseUI))]
    public class PauseUIEditor : UnityEditor.Editor
    {
        // MEMBERS: -------------------------------------------------------------------------------
        private VisualElement m_Root;
        // PAINT METHOD: --------------------------------------------------------------------------
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty pauseTime = this.serializedObject.FindProperty("m_PauseTime");
            SerializedProperty resumeTime = this.serializedObject.FindProperty("m_ResumeTime");
            SerializedProperty transitionDuration = this.serializedObject.FindProperty("m_TransitionDuration");
            SerializedProperty layer = this.serializedObject.FindProperty("m_Layer");

            PropertyField fieldResumeTime = new PropertyField(pauseTime);
            PropertyField fieldPauseTime = new PropertyField(resumeTime);
            PropertyField fieldTransitionDuration = new PropertyField(transitionDuration);
            PropertyField fieldLayer = new PropertyField(layer);

            this.m_Root.Add(fieldResumeTime);
            this.m_Root.Add(fieldPauseTime);
            this.m_Root.Add(fieldTransitionDuration);
            this.m_Root.Add(fieldLayer);

            return this.m_Root;
        }
        // CREATION MENU: -------------------------------------------------------------------------

        [MenuItem("GameObject/Game Creator/UI/Pause UI", false, 0)]
        public static void CreateElement(MenuCommand menuCommand)
        {
            GameObject canvas = UnityUIUtilities.GetCanvas();

            DefaultControls.Resources resources = UnityUIUtilities.GetStandardResources();
            GameObject gameObject = DefaultControls.CreateImage(resources);
            gameObject.transform.SetParent(canvas.transform, false);
            gameObject.name = "Pause UI";

            gameObject.AddComponent<PauseUI>();

            Undo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");
            Selection.activeGameObject = gameObject;
        }
    }
}