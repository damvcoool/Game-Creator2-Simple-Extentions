using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using SimpleExtentions.Runtime.SimpleMelee;

namespace SimpleExtentions.Editor.SimpleMelee
{
    [CustomEditor(typeof(SimpleMeleeWeapon))]
    public class SimpleMeleeWeaponEditor : UnityEditor.Editor
    {
        public static SimpleMeleeWeapon DEFAULT_INSTANCE;

        // MEMBERS: -------------------------------------------------------------------------------

        private VisualElement m_Root;
        private VisualElement m_WeaponData;
        private VisualElement m_Transform;
        private TextElement m_TransformLabel;
        private TextElement m_WeaponDataLabel;

        // PAINT METHODS: -------------------------------------------------------------------------

        public override VisualElement CreateInspectorGUI()
        {
            DEFAULT_INSTANCE = this.target as SimpleMeleeWeapon;
            this.m_Root = new VisualElement();
            this.m_Transform = new VisualElement();
            this.m_WeaponData = new VisualElement();
            this.m_TransformLabel = new TextElement();
            this.m_WeaponDataLabel = new TextElement();

            this.m_WeaponDataLabel.text = new string("Weapon Data");
            this.m_TransformLabel.text = new string("Transform Offet");
            
            SerializedProperty weaponPrefab = this.serializedObject.FindProperty("m_WeaponPrefab");
            SerializedProperty attackPower = this.serializedObject.FindProperty("m_AttackPower");
            SerializedProperty animationClip = this.serializedObject.FindProperty("m_AnimationClip");
            SerializedProperty avatarMask = this.serializedObject.FindProperty("m_AvatarMask");
            SerializedProperty useRootMotion = this.serializedObject.FindProperty("m_UseRootMotion");
            SerializedProperty bone = this.serializedObject.FindProperty("m_Bone");
            SerializedProperty position = this.serializedObject.FindProperty("m_Position");
            SerializedProperty rotation = this.serializedObject.FindProperty("m_Rotation");

            PropertyField fieldWeaponPrefab = new PropertyField(weaponPrefab);
            PropertyField fieldAttackPower = new PropertyField(attackPower);
            PropertyField fieldAnimationClip = new PropertyField(animationClip);
            PropertyField fieldAvatarMask = new PropertyField(avatarMask);
            PropertyField fieldUseRootMotion = new PropertyField(useRootMotion);
            PropertyField fieldBone = new PropertyField(bone);
            PropertyField fieldPosition = new PropertyField(position);
            PropertyField fieldRotation = new PropertyField(rotation);


            this.m_WeaponData.Add(m_WeaponDataLabel);
            this.m_WeaponData.Add(fieldWeaponPrefab);
            this.m_WeaponData.Add(fieldAttackPower);
            this.m_WeaponData.Add(fieldAnimationClip);
            this.m_WeaponData.Add(fieldAvatarMask);
            this.m_WeaponData.Add(fieldUseRootMotion);
            this.m_Transform.Add(m_TransformLabel);
            this.m_Transform.Add(fieldBone);
            this.m_Transform.Add(fieldPosition);
            this.m_Transform.Add(fieldRotation);
            
            this.m_Root.Add(m_WeaponData);
            this.m_Root.Add(m_Transform);

            return this.m_Root;
        }

        // DEFAULT INSTANCE METHODS: --------------------------------------------------------------

        [InitializeOnLoadMethod]
        private static void InitOnLoad()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SimpleMeleeWeapon)}");
            if (guids.Length == 0) return;

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            SimpleMeleeWeapon currency = AssetDatabase.LoadAssetAtPath<SimpleMeleeWeapon>(path);
            if (currency != null) DEFAULT_INSTANCE = currency;
        }
    }
}