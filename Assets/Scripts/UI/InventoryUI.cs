using UnityEngine;
using DungeonDice.Items;

namespace DungeonDice.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] GameObject inventoryObj;

        Inventory inventory;

        private void Awake() 
        {
            inventory = FindObjectOfType<Inventory>();
        }

        public void OpenInventory()
        {
            inventoryObj.SetActive(true);
            UpdateInventory();
        }

        public void CloseInventory()
        {
            inventoryObj.SetActive(false);
        }

        public void UpdateInventory()
        {

        }
    }
}
