using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Inventory
{
    [Version(1, 0, 0)]

    [Title("Inventory Has Item")]
    [Description("Returns true if the target Inventory holds at least the specified amount of an item")]

    [Category("Inventory/Has Item")]

    [Keywords("Inventory", "Item", "Check", "Has", "Count")]

    [Image(typeof(IconUnity), ColorTheme.Type.Yellow)]
    [Serializable]
    public class ConditionInventoryHasItem : Condition
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Inventory = GetGameObjectInstance.Create();
        [SerializeField] private InventoryItem m_Item;
        [SerializeField] private PropertyGetInteger m_Amount = GetDecimalInteger.Create(1);

        // PROPERTIES: ----------------------------------------------------------------------------

        protected override string Summary =>
            $"{m_Inventory} has {m_Amount} {(m_Item != null ? m_Item.DisplayName : "(none)")}";

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            Inventory inventory = m_Inventory.Get<Inventory>(args);
            if (inventory == null || m_Item == null) return false;
            return inventory.HasItem(m_Item, (int)m_Amount.Get(args));
        }
    }
}
