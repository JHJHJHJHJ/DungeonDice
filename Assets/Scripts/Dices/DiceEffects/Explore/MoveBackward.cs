using UnityEngine;
using DungeonDice.Characters;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "MoveBackward", menuName = "DungeonDice/Dice Effect/MoveBackward")]
    public class MoveBackward : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
            Player player = FindObjectOfType<Player>();
            player.StartCoroutine(player.Move(value, false));
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



