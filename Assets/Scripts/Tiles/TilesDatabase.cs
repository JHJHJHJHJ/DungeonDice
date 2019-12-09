using UnityEngine;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "TilesDatabase", menuName = "DungeonDice/New TilesDatabase", order = 0)]
    public class TilesDatabase : ScriptableObject
    {
        public GameObject[] tilePrefabs;
    }
}


