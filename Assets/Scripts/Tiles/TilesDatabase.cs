using UnityEngine;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "TilesDatabase", menuName = "DungeonDice/New TilesDatabase", order = 0)]
    public class TilesDatabase : ScriptableObject
    {
        public Tile[] normalTiles;
        public Tile[] stairTiles;
        public Tile[] monsterTiles;
        public Tile[] treasureTiles;
        public Tile[] etcTiles;
    }
}


