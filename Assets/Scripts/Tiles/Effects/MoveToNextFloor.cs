using UnityEngine;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "MoveToNextFloor", menuName = "DungeonDice/Tile Event Effect/MoveToNextFloor")]
    public class MoveToNextFloor : TileEventEffect
    {
        public override void Activate()
        {
            StateHolder stateHolder = FindObjectOfType<StateHolder>();
            stateHolder.SetFloor(stateHolder.GetCurrentFloor() + 1);
            FindObjectOfType<TilesContainer>().GenerateLevel(stateHolder.GetCurrentFloor());

            TilesContainer tilesContainer = FindObjectOfType<TilesContainer>();
            Player player = FindObjectOfType<Player>();

            for (int i = 0; i < tilesContainer.currentTileList.Count; i++)
            {
                if (i == player.currentTileIndex) continue;

                foreach (Transform child in tilesContainer.currentTileList[i].transform)
                {
                    if (!child.GetComponent<SpriteRenderer>()) continue;

                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.15f);
                }
            }

            player.UpdateCurrentTile();
        }
    }
}



