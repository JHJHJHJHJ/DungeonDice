using UnityEngine;
using DungeonDice.Stats;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "GenerateShield", menuName = "DungeonDice/Dice Effect/GenerateShield")]
    public class GenerateShield : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
            target.GetComponent<Shield>().AddShield(value);
        }

        public override bool isSelf()
        {
            return true;
        }

        public override string GetCombatMessage(string target, int value)
        {
            return target + "은 " + value.ToString() + "의 방어도를 쌓았다!";
        }
    }
}