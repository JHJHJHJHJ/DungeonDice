using UnityEngine;
using DungeonDice.Objects;
using DungeonDice.UI;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "MeetMonster", menuName = "DungeonDice/Tile Event Effect/Meet Monster")]
    public class MeetMonster : TileEventEffect
    {
        public override void Activate()
        {
            Enemy enemy = FindObjectOfType<Enemy>();

            string[] description = new string[1];
            description[0] = GetDescription(enemy);

            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);
        }

        string GetDescription(Enemy enemy)
        {
            string description = enemy.enemyName + "이(가) 나타났다!";
            return description;
        }
    }
}



