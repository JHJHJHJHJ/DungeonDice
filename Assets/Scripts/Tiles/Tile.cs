using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonDice.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileInfo tileInfo;

        private void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        }
    }

    [System.Serializable]
    public class TileInfo
    {
        public TileType tileType;
        public int number;
        public string name;
        public Ground ground;
        [TextArea]
        public string description;
        public TileEvent initialTileEvent;
    }
}

