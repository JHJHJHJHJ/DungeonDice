using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.UI;
using DungeonDice.Objects;
using DungeonDice.Items;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "BuyShopItem", menuName = "DungeonDice/Tile Event Effect/BuyShopItem")]
    public class BuyShopItem : TileEventEffect
    {
        public override void Activate()
        {
            FindObjectOfType<Player>().isShopping = false;

            Shop shop = FindObjectOfType<Shop>();

            string[] description = new string[1];
            description[0] = GetDescription(shop.itemList[shop.currentIndex]);
            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);

            shop.BuySelectedItem();
        }

        string GetDescription(Item selectedItem)
        {
            string description = selectedItem.itemName + "을(를) 획득했다!" + "\n" + selectedItem.price + "G를 지불했다!";
            return description;
        }
    }
}



