using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    public class TilesContainer : MonoBehaviour
    {
        [SerializeField] TilesDatabase tilesDatabase;
        [SerializeField] TileProgression tileProgression;
        [SerializeField] Transform[] spawnPositions;

        public List<Tile> currentTileList = new List<Tile>();

        private void Awake()
        {
            GenerateLevel(1);
        }

        public void GenerateLevel(int floorToGenerate)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Tile"))
                {
                    Destroy(child.gameObject);
                }
            }
            currentTileList.Clear();

            List<Tile> newTileList = MakeNewTileList(floorToGenerate);

            for (int i = 0; i < spawnPositions.Length; i++)
            {
                Tile newTile = Instantiate(newTileList[i], spawnPositions[i].transform.position, Quaternion.identity, transform);
                currentTileList.Add(newTile);
            }
        }

        private List<Tile> MakeNewTileList(int floorToGenerate)
        {
            List<Tile> newTileList = new List<Tile>();

            for (int i = 0; i < tileProgression.GetStairCount(floorToGenerate); i++)
            {
                Tile tileToAdd = tilesDatabase.stairTiles[Random.Range(0, tilesDatabase.stairTiles.Length)];
                newTileList.Add(tileToAdd);
            }
            for (int i = 0; i < tileProgression.GetMonsterCount(floorToGenerate); i++)
            {
                Tile tileToAdd = tilesDatabase.monsterTiles[Random.Range(0, tilesDatabase.monsterTiles.Length)];
                newTileList.Add(tileToAdd);
            }
            for (int i = 0; i < tileProgression.GetTreasureCount(floorToGenerate); i++)
            {
                Tile tileToAdd = tilesDatabase.treasureTiles[Random.Range(0, tilesDatabase.treasureTiles.Length)];
                newTileList.Add(tileToAdd);
            }

            while (newTileList.Count < 15)
            {
                Tile tileToAdd = tilesDatabase.normalTiles[Random.Range(0, tilesDatabase.normalTiles.Length)];
                newTileList.Add(tileToAdd);
            }

            ShuffleTiles(newTileList);

            AddNormalTileForPlayerTile(newTileList);

            return newTileList;
        }

        private void AddNormalTileForPlayerTile(List<Tile> newTileList)
        {
            newTileList.Add(tilesDatabase.normalTiles[Random.Range(0, tilesDatabase.normalTiles.Length)]);

            int currentPlayerTileIndex = FindObjectOfType<Player>().currentTileIndex;

            Tile temp = newTileList[15];
            newTileList[15] = newTileList[currentPlayerTileIndex];
            newTileList[currentPlayerTileIndex] = temp;
        }

        void ShuffleTiles(List<Tile> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                Tile temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }
    }
}

