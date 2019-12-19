using UnityEngine;
using System.Collections.Generic;

namespace DungeonDice.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int maxItemCount = 5;

        int selectedItemIndex;

        public List<Item> possessedItems;

        private void Start()
        {
            possessedItems = new List<Item>();
            possessedItems.Clear();
        }

        public void AddItem(Item itemToAdd)
        {
            if (possessedItems.Count < maxItemCount)
            {
                possessedItems.Add(itemToAdd);
            }
        }

        public void UseSelectedItem()
        {
            possessedItems[selectedItemIndex].itemEffect.Use();
            possessedItems.RemoveAt(selectedItemIndex);
        }

        public void UpdateSelectedIndex(int i)
        {
            selectedItemIndex = i;
        }
    }
}

