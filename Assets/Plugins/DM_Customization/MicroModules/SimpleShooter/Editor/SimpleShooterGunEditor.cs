using GameCreator.Editor.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using DM_Customization.Runtime.SimpleShooter;

namespace DM_Customization.Editor.SimpleShooter
{
    [CustomEditor(typeof(SimpleShooterGun))]

    public class SimpleShooterGunEditor : UnityEditor.Editor
    {
        public static SimpleShooterGun DEFAULT_INSTANCE;
        // MEMBERS: -------------------------------------------------------------------------------

        private VisualElement m_Root;
        private VisualElement m_GunData;
        private VisualElement m_MainHand;
        private VisualElement m_AimState;
        private VisualElement m_SecondHand;

        // PAINT METHODS: -------------------------------------------------------------------------

        public override VisualElement CreateInspectorGUI()
        {
            DEFAULT_INSTANCE = this.target as SimpleShooterGun;

            this.m_Root = new VisualElement();
            this.m_GunData = new VisualElement();
            this.m_AimState = new VisualElement();
            this.m_SecondHand = new VisualElement();
            this.m_MainHand = new VisualElement();

            //[SerializeField] private int m_Layer = 1;
            //[SerializeField] private BlendMode m_BlendMode = BlendMode.Blend;

            // Weapon Data
            SerializedProperty gunPrefab = this.serializedObject.FindProperty("m_GunPrefab");
            SerializedProperty bulletPrefab = this.serializedObject.FindProperty("m_BulletPrefab");

            // Animation Data
            SerializedProperty aimState = this.serializedObject.FindProperty("m_AimState");
            SerializedProperty layer = this.serializedObject.FindProperty("m_Layer");
            SerializedProperty blendMode = this.serializedObject.FindProperty("m_BlendMode");

            SerializedProperty mainHand = this.serializedObject.FindProperty("m_MainHand");
            SerializedProperty useSecondHand = this.serializedObject.FindProperty("m_UseSecondHand");
            SerializedProperty secondHand = this.serializedObject.FindProperty("m_SecondHand");

            // Placement Data
            SerializedProperty position = this.serializedObject.FindProperty("m_Position");
            SerializedProperty rotation = this.serializedObject.FindProperty("m_Rotation");

            PropertyTool fieldUseSecondHand = new PropertyTool(useSecondHand);

            this.m_GunData.Add(new LabelTitle("Gun & Bullet Data"));
            this.m_GunData.Add(new PropertyField(gunPrefab));
            this.m_GunData.Add(new PropertyField(bulletPrefab));
            

            this.m_AimState.Add(new LabelTitle("Animation State"));
            this.m_AimState.Add(new PropertyField(aimState));
            this.m_AimState.Add(new PropertyField(layer));
            this.m_AimState.Add(new PropertyField(blendMode));


            this.m_MainHand.Add(new LabelTitle("Gun & Bullet Data"));

            this.m_MainHand.Add(new PropertyField(mainHand));
            this.m_MainHand.Add(new PropertyField(position));
            this.m_MainHand.Add(new PropertyField(rotation));
            this.m_MainHand.Add(fieldUseSecondHand);
            this.m_SecondHand.Add(new PropertyField(secondHand));

            this.m_MainHand.Add(m_SecondHand);
            
            this.m_Root.Add(m_GunData);
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(m_AimState);
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(m_MainHand);

            //this.m_SecondHand.SetEnabled(useSecondHand.objectReferenceValue == false);
            //fieldUseSecondHand.EventChange += changeEvent =>
            //{
            //    bool exists = changeEvent.changedProperty.boolValue;
            //    this.m_SecondHand.SetEnabled(exists);
            //};

            return this.m_Root;
        }

        // DEFAULT INSTANCE METHODS: --------------------------------------------------------------

        [InitializeOnLoadMethod]
        private static void InitOnLoad()
        {
        }
    }
}