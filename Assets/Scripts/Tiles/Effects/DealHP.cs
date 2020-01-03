using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "DealHP", menuName = "DungeonDice/Tile Event Effect/DealHP")]
    public class DealHP : TileEventEffect
    {
        [SerializeField] int value;
        [SerializeField] bool isDestroying = false;

        public override void Activate()
        {
            HP hp = FindObjectOfType<Player>().GetComponent<HP>();
            hp.DealHP(value);

            if(isDestroying)
            {
                FindObjectOfType<Player>().currentTile.DestroyTileObject();
            }
        }
    }
}



