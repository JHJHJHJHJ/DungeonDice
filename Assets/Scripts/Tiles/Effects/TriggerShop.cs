using UnityEngine;
using DungeonDice.Items;
using DungeonDice.Tiles;
using DungeonDice.Characters;
using DungeonDice.Objects;
using DungeonDice.UI;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "TriggerShop", menuName = "DungeonDice/Tile Event Effect/TriggerShop")]
    public class TriggerShop : TileEventEffect
    {
        public override void Activate()
        {
            FindObjectOfType<Player>().isShopping = true;

            string[] description = new string[1];
            description[0] = GetDescription();

            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);
        }

        string GetDescription()
        {
            string description = "마음에 드는 아이템을 선택해주세요.";
            return description;
        }
    }
}



