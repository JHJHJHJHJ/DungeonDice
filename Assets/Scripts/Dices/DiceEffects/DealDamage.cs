using UnityEngine;
using DungeonDice.Stats;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "DealDamage", menuName = "DungeonDice/Dice Effect/DealDamage")]
    public class DealDamage : DiceEffect
    {
        public override void Activate(int value, GameObject target)
        {
            target.GetComponent<HP>().DealHP(-value);
        }

        public override string GetCombatMessage(string target, int value)
        {
            return target + "에게 " + value.ToString() + "의 데미지!";
        }
    }
}