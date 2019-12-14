using UnityEngine;

namespace DungeonDice.Items
{
    public class Item : MonoBehaviour 
    {
        public string itemName;
        [TextArea]
        public string description;
        public ItemEffect itemEffect;
    }
}

