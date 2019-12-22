using UnityEngine;
using DungeonDice.Items;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "InventoryIsEmpty", menuName = "DungeonDice/Event Move Condition/InventoryIsEmpty")]
    public class InventoryIsEmpty : MoveCondition
    {
        public override bool CanMove()
        {
            Inventory inventory = FindObjectOfType<Inventory>();

            foreach(Item item in inventory.myItems)
            {
                if(item == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}