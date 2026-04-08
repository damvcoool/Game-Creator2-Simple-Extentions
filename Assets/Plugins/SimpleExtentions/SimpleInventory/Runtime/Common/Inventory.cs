using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleExtentions.Runtime.Inventory
{
    [AddComponentMenu("Game Creator/Simple Extensions/Inventory")]
    public class Inventory : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private readonly Dictionary<InventoryItem, int> m_Items =
            new Dictionary<InventoryItem, int>();

        // EVENTS: --------------------------------------------------------------------------------

        /// <summary>Raised whenever an item's count changes. Arguments: inventory, item, new count.</summary>
        public static event Action<Inventory, InventoryItem, int> EventItemChanged;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        /// <summary>Returns the number of the specified item currently held.</summary>
        public int GetCount(InventoryItem item)
        {
            if (item == null) return 0;
            return m_Items.TryGetValue(item, out int count) ? count : 0;
        }

        /// <summary>Returns true when at least <paramref name="amount"/> of the item is held.</summary>
        public bool HasItem(InventoryItem item, int amount = 1)
        {
            return GetCount(item) >= amount;
        }

        /// <summary>Adds <paramref name="amount"/> copies of <paramref name="item"/> to this inventory.</summary>
        public void AddItem(InventoryItem item, int amount = 1)
        {
            if (item == null || amount <= 0) return;
            m_Items.TryGetValue(item, out int current);
            m_Items[item] = current + amount;
            EventItemChanged?.Invoke(this, item, m_Items[item]);
        }

        /// <summary>
        /// Removes up to <paramref name="amount"/> copies of <paramref name="item"/>.
        /// The count will never drop below zero.
        /// </summary>
        public void RemoveItem(InventoryItem item, int amount = 1)
        {
            if (item == null || amount <= 0) return;
            if (!m_Items.TryGetValue(item, out int current)) return;
            int newCount = Mathf.Max(0, current - amount);
            if (newCount == 0)
                m_Items.Remove(item);
            else
                m_Items[item] = newCount;
            EventItemChanged?.Invoke(this, item, newCount);
        }
    }
}
