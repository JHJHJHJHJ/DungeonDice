using UnityEngine;
using DungeonDice.Items;

namespace DungeonDice.Objects
{
    public class Treasure : MonoBehaviour
    {
        [SerializeField] ItemDatabase itemDatabase;

        [HideInInspector] public Item itemInside;

        private void Start() 
        {
            SetItem();    
        }

        void SetItem()
        {
            itemInside = itemDatabase.items[Random.Range(0, itemDatabase.items.Length)];
        }
    }
}
