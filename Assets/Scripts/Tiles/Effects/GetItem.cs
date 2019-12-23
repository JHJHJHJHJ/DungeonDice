using UnityEngine;
using DungeonDice.Items;
using DungeonDice.Characters;
using DungeonDice.Objects;
using DungeonDice.UI;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "GetItem", menuName = "DungeonDice/Tile Event Effect/GetItem")]
    public class GetItem : TileEventEffect
    {
        public override void Activate()
        {
            Tile currentTile = FindObjectOfType<Player>().currentTile;

            Item itemToGet = currentTile.GetComponent<Treasure>().itemInside;
            FindObjectOfType<Inventory>().GetItem(itemToGet);

            currentTile.DestroyTileObject();

            string[] description = new string[1];
            description[0] = GetDescription(itemToGet);

            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);
        }

        string GetDescription(Item itemToGet)
        {
            string description = itemToGet.itemName + "을(를) 획득했다!";
            return description;
        }
    }
}



