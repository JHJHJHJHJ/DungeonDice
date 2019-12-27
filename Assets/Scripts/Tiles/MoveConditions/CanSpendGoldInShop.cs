using UnityEngine;
using DungeonDice.Items;
using DungeonDice.Objects;
using DungeonDice.Stats;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "CanSpendGoldInShop", menuName = "DungeonDice/Event Move Condition/CanSpendGoldInShop")]
    public class CanSpendGoldInShop : MoveCondition
    {
        public override bool CanMove()
        {
            Shop shop = FindObjectOfType<Shop>();
            int priceToSpend = shop.itemList[shop.currentIndex].price;

            return FindObjectOfType<Gold>().CanSpend(priceToSpend);
        }
    }
}