using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Inventory
{
    [Version(1, 0, 0)]

    [Title("Remove Item from Inventory")]
    [Description("Removes a specified number of an item from the target Inventory component")]

    [Category("Inventory/Remove Item")]

    [Parameter("Inventory", "The GameObject that holds the Inventory component")]
    [Parameter("Item", "The Inventory Item asset to remove")]
    [Parameter("Amount", "The number of items to remove")]

    [Keywords("Inventory", "Item", "Remove", "Drop", "Use", "Consume")]

    [Image(typeof(IconUnity), ColorTheme.Type.Red)]
    [Serializable]
    public class InstructionInventoryRemoveItem : Instruction
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Inventory = GetGameObjectInstance.Create();
        [SerializeField] private InventoryItem m_Item;
        [SerializeField] private PropertyGetInteger m_Amount = GetDecimalInteger.Create(1);

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title =>
            $"Remove {(m_Item != null ? m_Item.DisplayName : "(none)")} from {m_Inventory}";

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            Inventory inventory = m_Inventory.Get<Inventory>(args);
            if (inventory == null || m_Item == null) return DefaultResult;

            inventory.RemoveItem(m_Item, (int)m_Amount.Get(args));
            return DefaultResult;
        }
    }
}
