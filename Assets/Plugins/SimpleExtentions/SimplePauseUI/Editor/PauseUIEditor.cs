using GameCreator.Editor.Common.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using SimpleExtentions.Runtime.Pause;

namespace SimpleExtentions.Editor.Pause
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

        [MenuItem("GameObject/Game Creator/Simple Extensions/Simple Pause UI", false, 0)]
        public static void CreateElement(MenuCommand menuCommand)
        {
            GameObject canvasObj = new GameObject("PauseCanvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(PauseUI));
            Canvas canvas = canvasObj.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            DefaultControls.Resources resources = UnityUIUtilities.GetStandardResources();
            GameObject gameObject = DefaultControls.CreateImage(resources);
            gameObject.transform.SetParent(canvasObj.transform, false);
            gameObject.name = "Pause UI";

            Undo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");
            Selection.activeGameObject = canvasObj;
        }
    }
}