using UnityEngine;
using DungeonDice.Objects;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "ShopIsEmpty", menuName = "DungeonDice/Event Branch/ShopIsEmpty")]
    public class ShopIsEmpty : EventBranch
    {
        public override int GetBranch(int value)
        {
            Shop shop = FindObjectOfType<Shop>();
            if(shop.isEmpty())
            {
                return 0;
            }
            return 1;
        }
    } 
}