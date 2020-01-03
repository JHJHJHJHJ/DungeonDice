using UnityEngine;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "LevelProgression", menuName = "DungeonDice/Level Progression", order = 0)]
    public class TileProgression : ScriptableObject
    {
        [SerializeField] int[] progressionScale;
        [SerializeField] tileTypeCounts[] tileCounts;

        public int GetStairCount(int floorToGenerate)
        {
            int index = GetTileCountIndex(floorToGenerate);
            return tileCounts[index].stairCount;
        }

        public int GetMonsterCount(int floorToGenerate)
        {
            int index = GetTileCountIndex(floorToGenerate);
            return tileCounts[index].monsterCount;
        }

        public int GetTreasureCount(int floorToGenerate)
        {
            int index = GetTileCountIndex(floorToGenerate);
            return tileCounts[index].treasureCount;
        }

        public int GetetcCount(int floorToGenerate)
        {
            int index = GetTileCountIndex(floorToGenerate);
            return tileCounts[index].etcCount;
        }

        int GetTileCountIndex(int floor)
        {
            for (int i = 0; i < progressionScale.Length; i++)
            {
                if(floor < progressionScale[i])
                {
                    return i;
                }
            }

            return 0;
        }
    }

    [System.Serializable]
    public class tileTypeCounts
    {
        public int stairCount;
        public int monsterCount;
        public int treasureCount;
        public int etcCount;
    }
}
