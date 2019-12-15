using UnityEngine;
using DungeonDice.Items;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "GetItem", menuName = "DungeonDice/Tile Event Effect/GetItem")]
    public class GetItem : TileEventEffect
    {
        [SerializeField] Item itemToAdd;

        public override void Activate()
        {
            FindObjectOfType<Inventory>().AddItem(itemToAdd);
        }
    }
}



