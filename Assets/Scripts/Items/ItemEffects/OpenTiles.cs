using UnityEngine;
using DungeonDice.Tiles;
using System.Collections.Generic;

namespace DungeonDice.Items
{
    [CreateAssetMenu(fileName = "OpenTiles", menuName = "DungeonDice/Item Effect/OpenTiles")]
    public class OpenTiles : ItemEffect
    {
        [SerializeField] int number;

        public override void Use()
        {
            TilesContainer tilesContainer = FindObjectOfType<TilesContainer>();

            List<int> hiddenTileIndexs = new List<int>();

            for(int i = 0; i <tilesContainer.currentTileList.Count; i++)
            {
                Tile tile = tilesContainer.currentTileList[i];
                if(tile.isHidden == true)
                {
                    hiddenTileIndexs.Add(i);
                }
            }

            ShuffleInts(hiddenTileIndexs);

            for(int i = 0; i < number; i++)
            {
                if(i >= hiddenTileIndexs.Count) return;

                tilesContainer.currentTileList[hiddenTileIndexs[i]].Open();
            }
        }

        void ShuffleInts(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                int temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }
    }
}