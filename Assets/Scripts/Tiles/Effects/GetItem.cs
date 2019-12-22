using UnityEngine;
using DungeonDice.Items;
using DungeonDice.Characters;
using DungeonDice.Objects;
using DungeonDice.Tiles;

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
        }
    }
}



