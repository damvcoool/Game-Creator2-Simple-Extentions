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
        private VisualElement m_Transform;
        private VisualElement m_AimState;
        private VisualElement m_SecondHand;
        private TextElement m_TransformLabel;
        private TextElement m_AimingStateLabel;
        private TextElement m_GunDataLabel;

        private VisualElement m_TransitionOptions;


        // PAINT METHODS: -------------------------------------------------------------------------

        public override VisualElement CreateInspectorGUI()
        {
            DEFAULT_INSTANCE = this.target as SimpleShooterGun;

            this.m_Root = new VisualElement();
            this.m_GunData = new VisualElement();
            this.m_AimState = new VisualElement();
            this.m_SecondHand = new VisualElement();
            this.m_Transform = new VisualElement();

            this.m_TransformLabel = new TextElement();
            this.m_AimingStateLabel = new TextElement();  
            this.m_GunDataLabel = new TextElement();

            this.m_GunDataLabel.text = new string("Gun & Bullet Data");
            this.m_TransformLabel.text = new string("Hand Placement");
            this.m_AimingStateLabel.text = new string("Aiming State Data");


            //[SerializeField] private State m_AimState;
            //[SerializeField] private int m_Layer = 1;
            //[SerializeField] private BlendMode m_BlendMode = BlendMode.Blend;

            
            // Weapon Data
            SerializedProperty gunPrefab = this.serializedObject.FindProperty("m_GunPrefab");
            SerializedProperty bulletPrefab = this.serializedObject.FindProperty("m_BulletPrefab");

            // Animation Data
            SerializedProperty mainHand = this.serializedObject.FindProperty("m_MainHand");
            SerializedProperty useSecondHand = this.serializedObject.FindProperty("m_UseSecondHand");
            SerializedProperty secondHand = this.serializedObject.FindProperty("m_SecondHand");

            // Placement Data
            SerializedProperty position = this.serializedObject.FindProperty("m_Position");
            SerializedProperty rotation = this.serializedObject.FindProperty("m_Rotation");


            //w
            PropertyField fieldGunPrefab = new PropertyField(gunPrefab);
            PropertyField fieldBulletPrefab = new PropertyField(bulletPrefab);

            //A
            PropertyField fieldMainHand = new PropertyField(mainHand);
            PropertyTool fieldUseSecondHand = new PropertyTool(useSecondHand);
            PropertyField fieldSecondHand = new PropertyField(secondHand);

            //P
            PropertyField fieldPosition = new PropertyField(position);
            PropertyField fieldRotation = new PropertyField(rotation);

            this.m_GunData.Add(m_GunDataLabel);
            this.m_GunData.Add(fieldGunPrefab);
            this.m_GunData.Add(fieldBulletPrefab);

            this.m_AimState.Add(m_AimingStateLabel);
            this.m_AimState.Add(fieldMainHand);
            this.m_AimState.Add(fieldUseSecondHand);
            this.m_SecondHand.Add(fieldSecondHand);

            this.m_Transform.Add(m_TransformLabel);
            this.m_Transform.Add(fieldPosition);
            this.m_Transform.Add(fieldRotation);

            this.m_AimState.Add(m_SecondHand);
            this.m_Root.Add(m_GunData);
            this.m_Root.Add(m_AimState);
            this.m_Root.Add(m_Transform);

            this.m_SecondHand.SetEnabled(useSecondHand.objectReferenceValue == false);
            fieldUseSecondHand.EventChange += changeEvent =>
            {
                bool exists = changeEvent.changedProperty.boolValue;
                this.m_SecondHand.SetEnabled(exists);
            };

            return this.m_Root;
        }

        // DEFAULT INSTANCE METHODS: --------------------------------------------------------------

        [InitializeOnLoadMethod]
        private static void InitOnLoad()
        {
        }
    }
}