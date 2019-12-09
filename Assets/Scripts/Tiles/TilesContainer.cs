using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonDice.Tiles
{
    public class TilesContainer : MonoBehaviour
    {
        [SerializeField] TilesDatabase tilesDatabase;
        [SerializeField] Transform[] spawnPositions;

        public List<Tile> currentTileList = new List<Tile>();

        private void Awake()
        {
            SpawnRandomTiles();
        }

        public void SpawnRandomTiles()
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Tile"))
                {
                    Destroy(child.gameObject);
                }
            }
            currentTileList.Clear();

            foreach (Transform spawnPos in spawnPositions)
            {
                Tile tileToSpawn = tilesDatabase.tilePrefabs[Random.Range(0, tilesDatabase.tilePrefabs.Length)].GetComponent<Tile>();
                Tile newTile = Instantiate(tileToSpawn, spawnPos.position, Quaternion.identity, transform);

                currentTileList.Add(newTile);
            }
        }
    }
}

