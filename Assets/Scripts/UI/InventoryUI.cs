using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonDice.Items;
using TMPro;

namespace DungeonDice.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] GameObject detailWindow;
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemDescriptionText;

        Inventory inventory;

        int selectIndex;

        private void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        public void SelectItem(int i)
        {
            if(inventory.myItems[i] == null) return;

            selectIndex = i;
            
            detailWindow.SetActive(true);
            UpdateDescription(i);
        }

        void UpdateDescription(int i)
        {
            itemNameText.text = inventory.myItems[i].itemName;
            itemDescriptionText.text = inventory.myItems[i].description;
        }

        public void UseSelectedItem()
        {
            inventory.UseItem(selectIndex);

            ShutDetailWindow();
        }

        public void ShutDetailWindow()
        {
            detailWindow.SetActive(false);
        }
    }
}