using UnityEngine;
using DungeonDice.Characters;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "MovePlayer", menuName = "DungeonDice/Dice Effect/MovePlayer")]
    public class MovePlayer : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
            Player player = FindObjectOfType<Player>();
            player.StartCoroutine(player.Move(value));
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



