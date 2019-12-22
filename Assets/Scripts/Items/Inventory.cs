using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DungeonDice.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int maxItemCount = 5;
        [SerializeField] Image[] itemImages;

        public List<Item> myItems;

        private void Start()
        {
            myItems = new List<Item>();
            myItems.Clear();

            InitializeInventory();
        }

        void InitializeInventory()
        {
            for (int i = 0; i < maxItemCount; i++)
            {
                myItems.Add(null);
            }

            UpdateItemImages();
        }

        public void GetItem(Item itemToAdd)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i] == null)
                {
                    myItems.RemoveAt(i);
                    myItems.Insert(i, itemToAdd);
                    UpdateItemImages();

                    return;
                }

                continue;
            }
        }

        public void UseItem(int i)
        {
            myItems[i].itemEffect.Use();
            myItems.RemoveAt(i);
            myItems.Insert(i, null);

            UpdateItemImages();
        }

        void UpdateItemImages()
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i] != null)
                {
                    itemImages[i].gameObject.SetActive(true);
                    itemImages[i].sprite = myItems[i].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    itemImages[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

