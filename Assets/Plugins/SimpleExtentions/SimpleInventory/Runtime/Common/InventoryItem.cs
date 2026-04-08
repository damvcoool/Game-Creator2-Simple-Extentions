using UnityEngine;

namespace SimpleExtentions.Runtime.Inventory
{
    [CreateAssetMenu(
        fileName = "InventoryItem",
        menuName = "Game Creator/Simple Extensions/Inventory Item"
    )]
    public class InventoryItem : ScriptableObject
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private string m_DisplayName = "Item";
        [SerializeField][TextArea] private string m_Description = "";
        [SerializeField] private Sprite m_Icon;

        // PROPERTIES: ----------------------------------------------------------------------------

        public string DisplayName => m_DisplayName;
        public string Description => m_Description;
        public Sprite Icon => m_Icon;
    }
}
