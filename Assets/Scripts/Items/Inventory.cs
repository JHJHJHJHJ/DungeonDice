using UnityEngine;
using System.Collections.Generic;

namespace DungeonDice.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int maxItemCount = 5;

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

        public void UseItem(int itemIndex)
        {
            possessedItems[itemIndex].itemEffect.Use();
            possessedItems.RemoveAt(itemIndex);
        }
    }
}

