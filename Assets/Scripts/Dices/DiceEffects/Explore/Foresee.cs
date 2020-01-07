using UnityEngine;
using DungeonDice.Characters;
using DungeonDice.Stats;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "Foresee", menuName = "DungeonDice/Dice Effect/Foresee")]
    public class Foresee : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
            Player player = FindObjectOfType<Player>();
            player.SetForesee(value);

            string message = "예언" + value.ToString();

            FindObjectOfType<GameMessage>().SetMessage(message);

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



