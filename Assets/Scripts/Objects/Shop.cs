using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Items;
using DungeonDice.Characters;
using DungeonDice.Stats;

namespace DungeonDice.Objects
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] ItemDatabase itemDatabase;
        public List<Item> itemList;

        public int currentIndex;

        private void Start()
        {
            InitialzieItemList();
        }

        void InitialzieItemList()
        {
            itemList = new List<Item>();
            for (int i = 0; i < 3; i++)
            {
                Item itemToAdd = itemDatabase.items[Random.Range(0, itemDatabase.items.Length)];
                itemList.Add(itemToAdd);
            }
        }

        public void UpdateShopItemSprites()
        {
            Ground ground = FindObjectOfType<Ground>();

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i] == null)
                {
                    ground.shopItemObjects[i].gameObject.SetActive(false);
                }
                else
                {
                    ground.shopItemObjects[i].GetComponent<SpriteRenderer>().sprite = itemList[i].GetComponent<SpriteRenderer>().sprite;
                }
            }
        }

        public void SetCurrentIndex(int index)
        {
            currentIndex = index;
        }

        public void BuySelectedItem()
        {
            Item currentItem = itemList[currentIndex];
            FindObjectOfType<Inventory>().GetItem(currentItem);
            FindObjectOfType<Gold>().SpendGold(currentItem.price);

            itemList[currentIndex] = null;
            FindObjectOfType<Ground>().shopItemObjects[currentIndex].SetActive(false);

            if(isEmpty())
            {
                FindObjectOfType<Player>().currentTile.Change();
            }
        }

        public bool isEmpty()
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                if(itemList[i] != null) return false;
            }
            return true;
        }
    }
}
