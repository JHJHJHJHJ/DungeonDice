using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonDice.Items;
using TMPro;

namespace DungeonDice.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] GameObject inventoryObj;
        [SerializeField] Image[] itemImages;
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemDescriptionText;

        Inventory inventory;

        private void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        public void OpenInventory()
        {
            inventoryObj.SetActive(true);
            UpdateItemImages();
        }

        void UpdateItemImages()
        {
            foreach (Image itemImage in itemImages)
            {
                itemImage.gameObject.SetActive(false);
            }

            List<Item> possessedItems = inventory.possessedItems;

            for (int i = 0; i < possessedItems.Count; i++)
            {
                itemImages[i].gameObject.SetActive(true);
                itemImages[i].sprite = possessedItems[i].GetComponent<SpriteRenderer>().sprite;
            }

            itemNameText.text = "";
            itemDescriptionText.text = "";
        }

        public void SelectItem(int i)
        {
            Item selectedItem = inventory.possessedItems[i];

            inventory.UpdateSelectedIndex(i);

            itemNameText.text = selectedItem.itemName;
            itemDescriptionText.text = selectedItem.description;
        }

        public void CloseInventory()
        {
            inventoryObj.SetActive(false);
        }

        public void UseItem()
        {
            inventory.UseSelectedItem();
            UpdateItemImages();
        }
    }
}
