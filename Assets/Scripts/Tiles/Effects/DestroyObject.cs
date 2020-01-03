using UnityEngine;
using DungeonDice.Characters;
using DungeonDice.Tiles;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "DestroyObject", menuName = "DungeonDice/Tile Event Effect/DestroyObject")]
    public class DestroyObject : TileEventEffect
    {
        public override void Activate()
        {
            FindObjectOfType<Player>().currentTile.DestroyTileObject();
        }
    }
}



