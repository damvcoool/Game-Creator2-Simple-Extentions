using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters.IK;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(AimTrack))]
    public class AimTrackDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {

            SerializedProperty trackSpeed = property.FindPropertyRelative("m_TrackSpeed");
            SerializedProperty maxAngle = property.FindPropertyRelative("m_MaxAngle");
            SerializedProperty handWeight = property.FindPropertyRelative("m_RHandWeight");

            VisualElement root = new VisualElement();

            root.Add(new PropertyTool(trackSpeed));
            root.Add(new PropertyTool(maxAngle));
            root.Add(new PropertyTool(handWeight));

            return root;
        }
    }
}