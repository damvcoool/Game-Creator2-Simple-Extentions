using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Inventory
{
    [Version(1, 0, 0)]

    [Title("Add Item to Inventory")]
    [Description("Adds a specified number of an item to the target Inventory component")]

    [Category("Inventory/Add Item")]

    [Parameter("Inventory", "The GameObject that holds the Inventory component")]
    [Parameter("Item", "The Inventory Item asset to add")]
    [Parameter("Amount", "The number of items to add")]

    [Keywords("Inventory", "Item", "Add", "Pickup", "Collect")]

    [Image(typeof(IconUnity), ColorTheme.Type.Green)]
    [Serializable]
    public class InstructionInventoryAddItem : Instruction
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Inventory = GetGameObjectInstance.Create();
        [SerializeField] private InventoryItem m_Item;
        [SerializeField] private PropertyGetInteger m_Amount = GetDecimalInteger.Create(1);

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title =>
            $"Add {(m_Item != null ? m_Item.DisplayName : "(none)")} to {m_Inventory}";

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            Inventory inventory = m_Inventory.Get<Inventory>(args);
            if (inventory == null || m_Item == null) return DefaultResult;

            inventory.AddItem(m_Item, (int)m_Amount.Get(args));
            return DefaultResult;
        }
    }
}
