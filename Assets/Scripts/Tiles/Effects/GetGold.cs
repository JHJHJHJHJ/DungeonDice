using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.UI;
using DungeonDice.Objects;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "GetGold", menuName = "DungeonDice/Tile Event Effect/GetGold")]
    public class GetGold : TileEventEffect
    {
        public override void Activate()
        {
            Enemy enemy = FindObjectOfType<Enemy>();

            int goldToGet = Random.Range(enemy.minGold, enemy.maxGold + 1);

            FindObjectOfType<Gold>().GetGold(goldToGet);

            string[] description = new string[1];
            description[0] = GetDescription(goldToGet);

            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);
        }

        string GetDescription(int goldToGet)
        {
            string description = goldToGet + "G를 획득했다!";
            return description;
        }
    }
}



