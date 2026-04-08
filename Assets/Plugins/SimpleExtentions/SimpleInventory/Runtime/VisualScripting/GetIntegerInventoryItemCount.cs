using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.Inventory
{
    [Title("Inventory Item Count")]
    [Category("Inventory/Item Count")]

    [Image(typeof(IconUnity), ColorTheme.Type.TextLight)]
    [Description("Returns the count of a specific item held in the target Inventory component")]

    [Keywords("Inventory", "Item", "Count", "Amount")]

    [Serializable]
    public class GetIntegerInventoryItemCount : PropertyTypeGetInteger
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Inventory = GetGameObjectInstance.Create();
        [SerializeField] private InventoryItem m_Item;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string String =>
            $"Count {(m_Item != null ? m_Item.DisplayName : "(none)")}";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override long Get(Args args)
        {
            Inventory inventory = m_Inventory.Get<Inventory>(args);
            if (inventory == null || m_Item == null) return 0;
            return inventory.GetCount(m_Item);
        }

        public override long Get(GameObject gameObject)
        {
            Inventory inventory = gameObject.GetComponent<Inventory>();
            if (inventory == null || m_Item == null) return 0;
            return inventory.GetCount(m_Item);
        }

        public static PropertyGetInteger Create => new PropertyGetInteger(
            new GetIntegerInventoryItemCount()
        );
    }
}
