using UnityEngine;
using DungeonDice.Items;
using DungeonDice.Tiles;
using DungeonDice.Characters;
using DungeonDice.Objects;
using DungeonDice.UI;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "OpenTreasureBox", menuName = "DungeonDice/Tile Event Effect/OpenTreasureBox")]
    public class OpenTreasureBox : TileEventEffect
    {
        public override void Activate()
        {
            Treasure treasure = FindObjectOfType<Player>().currentTile.GetComponent<Treasure>();

            string[] description = new string[1];
            description[0] = GetItemDescription(treasure.itemInside);

            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);

            GameObject itemSprite = FindObjectOfType<Ground>().additionalObject;

            itemSprite.SetActive(true);
            itemSprite.GetComponent<SpriteRenderer>().sprite = treasure.itemInside.GetComponent<SpriteRenderer>().sprite;
        }

        string GetItemDescription(Item itemInside)
        {
            string description = itemInside.itemName + "이(가) 들어있다!" + "\n\n" + "* " + itemInside.description;
            return description;
        }
    }
}



