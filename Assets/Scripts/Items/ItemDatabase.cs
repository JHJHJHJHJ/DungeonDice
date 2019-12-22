using UnityEngine;

namespace DungeonDice.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "DungeonDice/New ItemDatabase", order = 0)]
    public class ItemDatabase : ScriptableObject
    {
        public Item[] items;
    }
}
