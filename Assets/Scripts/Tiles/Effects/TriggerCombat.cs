using UnityEngine;
using DungeonDice.Characters;
using DungeonDice.UI;
using DungeonDice.Combat;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "TriggerCombat", menuName = "DungeonDice/Tile Event Effect/TriggerCombat")]
    public class TriggerCombat : TileEventEffect
    {
        public override void Activate()
        {
            FindObjectOfType<EventTextBox>().Close();

            StateHolder stateHolder = FindObjectOfType<StateHolder>();
            stateHolder.SetPhaseToCombat();

            CombatManager combatManager = FindObjectOfType<CombatManager>();
            combatManager.StartCoroutine(combatManager.InitializeCombat());
        }
    }
}



