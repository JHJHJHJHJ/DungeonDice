using UnityEngine;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "MoveToNextFloor", menuName = "DungeonDice/Tile Event Effect/MoveToNextFloor")]
    public class MoveToNextFloor : TileEventEffect
    {
        public override void Activate()
        {
            PhaseManager phaseManager = FindObjectOfType<PhaseManager>();
            phaseManager.SetFloor(phaseManager.GetCurrentFloor() + 1);
            FindObjectOfType<TilesContainer>().GenerateLevel(phaseManager.GetCurrentFloor());

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



