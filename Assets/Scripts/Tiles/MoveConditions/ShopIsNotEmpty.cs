using UnityEngine;
using DungeonDice.Objects;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "ShopIsNotEmpty", menuName = "DungeonDice/Event Move Condition/ShopIsNotEmpty")]
    public class ShopIsNotEmpty : MoveCondition
    {
        public override bool CanMove()
        {
            Shop shop = FindObjectOfType<Player>().currentTile.GetComponent<Shop>();
            
            return !shop.isEmpty();
        }
    }
}