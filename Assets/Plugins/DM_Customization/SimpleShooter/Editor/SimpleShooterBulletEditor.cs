using GameCreator.Editor.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using DM_Customization.Runtime.SimpleShooter;

namespace DM_Customization.Editor.SimpleShooter
{
    [CustomEditor(typeof(SimpleShooterBullet))]
    public class SimpleShooterBulletEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;

        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty bulletSpeed = this.serializedObject.FindProperty("m_BulletSpeed");
            SerializedProperty onBulletHit = this.serializedObject.FindProperty("m_OnBulletHit");
            SerializedProperty destroyOnImpact = this.serializedObject.FindProperty("m_DestroyOnImpact");

            this.m_Root.Add(new PropertyField(bulletSpeed));
            this.m_Root.Add(new SpaceSmallest());
            this.m_Root.Add(new LabelTitle("On Bullet Hit Target"));
            this.m_Root.Add(new SpaceSmallest());
            this.m_Root.Add(new PropertyField(onBulletHit));
            this.m_Root.Add(new PropertyTool(destroyOnImpact));

            return this.m_Root;
        }

    }
}