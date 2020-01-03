using UnityEngine;

namespace DungeonDice.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "DungeonDice/Item Database", order = 0)]
    public class ItemDatabase : ScriptableObject
    {
        public Item[] items;
    }
}
