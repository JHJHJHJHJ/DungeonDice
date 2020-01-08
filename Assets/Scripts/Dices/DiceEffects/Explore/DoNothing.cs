using UnityEngine;
using DungeonDice.Characters;
using DungeonDice.Stats;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "DoNothing", menuName = "DungeonDice/Dice Effect/DoNothing")]
    public class DoNothing : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
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



