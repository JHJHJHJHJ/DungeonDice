using UnityEngine;
using DungeonDice.Characters;
using DungeonDice.Stats;
using DungeonDice.Tiles;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "Teleport", menuName = "DungeonDice/Dice Effect/Teleport")]
    public class Teleport : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
            FindObjectOfType<TileSelector>().ActivateTileSelector(TeleportPlayerToSelectedTile, 1);
        }

        void TeleportPlayerToSelectedTile(Tile tile)
        {
            Player player = FindObjectOfType<Player>();
            player.transform.position = tile.transform.position;
            player.currentTileIndex = tile.index;
            player.UpdateCurrentTile();

            FindObjectOfType<StateHolder>().SetPhaseToEvent();
        }

        public override bool isSelf()
        {
            return false;
        }
        
        public override string GetCombatMessage(string target, int value)
        {
            return null; 
        }
    }
}



