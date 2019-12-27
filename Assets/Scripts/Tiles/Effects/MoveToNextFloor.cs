using UnityEngine;
using DungeonDice.UI;
using DungeonDice.Characters;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "MoveToNextFloor", menuName = "DungeonDice/Tile Event Effect/MoveToNextFloor")]
    public class MoveToNextFloor : TileEventEffect
    {
        public override void Activate()
        {
            StateHolder stateHolder = FindObjectOfType<StateHolder>();
            TilesContainer tilesContainer = FindObjectOfType<TilesContainer>();
            Player player = FindObjectOfType<Player>();

            stateHolder.SetFloor(stateHolder.GetCurrentFloor() + 1);
            tilesContainer.GenerateLevel(stateHolder.GetCurrentFloor());

            string[] description = new string[1];
            description[0] = GetDescription(stateHolder);
            FindObjectOfType<EventTextBox>().EnqueueDescriptions(description);

            for (int i = 0; i < tilesContainer.currentTileList.Count; i++)
            {
                if (i == player.currentTileIndex) continue;

                Tile currentTile = tilesContainer.currentTileList[i];

                foreach (Transform child in tilesContainer.currentTileList[i].transform)
                {
                    if (!child.GetComponent<SpriteRenderer>()) continue;

                    float tileColor = currentTile.spriteColor;
                    child.GetComponent<SpriteRenderer>().color = new Color(tileColor, tileColor, tileColor, 0.15f);
                }
            }

            player.UpdateCurrentTile();
        }

        string GetDescription(StateHolder stateHolder)
        {
            string description = stateHolder.GetCurrentFloor().ToString() + "층에 도착했다!";
            return description;
        }
    }
}



