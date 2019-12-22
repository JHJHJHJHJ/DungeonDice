using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonDice.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileInfo tileInfo;
        Transform tileObject;

        private void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if(child.CompareTag("TileObject"))
                {
                    tileObject = child;
                }
            }
        }

        public void DestroyTileObject()
        {
            Destroy(tileObject.gameObject);
            tileInfo.initialTileEvent = null;
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

