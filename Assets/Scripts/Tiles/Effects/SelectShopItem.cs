using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.UI;
using DungeonDice.Objects;
using DungeonDice.Characters;
using DungeonDice.Items;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "SelectShopItem", menuName = "DungeonDice/Tile Event Effect/SelectShopItem")]
    public class SelectShopItem : TileEventEffect
    {
        public override void Activate()
        {
            Shop shop = FindObjectOfType<Player>().currentTile.GetComponent<Shop>();
            
            Item selectedItem = shop.itemList[shop.currentIndex];

            string[] description = new string[1];
            description[0] = GetDescription(selectedItem);

            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);
        }

        string GetDescription(Item selectedItem)
        {
            string description = selectedItem.itemName + "  " + selectedItem.price + "G" + "\n\n" + "* " + selectedItem.description;
            return description;
        }
    }
}



