using GameCreator.Editor.Common;
using DM_Customization.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace DM_Customization.Editor
{
    [CustomEditor(typeof(SimpleAttack))]
    public class SimpleAttackEditor : UnityEditor.Editor
    {
        public static SimpleAttack DEFAULT_INSTANCE;

        // MEMBERS: -------------------------------------------------------------------------------

        private VisualElement m_Root;

        // PAINT METHODS: -------------------------------------------------------------------------

        public override VisualElement CreateInspectorGUI()
        {
            DEFAULT_INSTANCE = this.target as SimpleAttack;
            this.m_Root = new VisualElement();

            SerializedProperty weaponPrefab = this.serializedObject.FindProperty("m_Attack");
            
            PropertyField fieldWeaponPrefab = new PropertyField(weaponPrefab);
            
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(fieldWeaponPrefab);
            
            return this.m_Root;
        }

        // DEFAULT INSTANCE METHODS: --------------------------------------------------------------

        [InitializeOnLoadMethod]
        private static void InitOnLoad()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SimpleAttack)}");
            if (guids.Length == 0) return;

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            SimpleAttack currency = AssetDatabase.LoadAssetAtPath<SimpleAttack>(path);
            if (currency != null) DEFAULT_INSTANCE = currency;
        }
    }
}