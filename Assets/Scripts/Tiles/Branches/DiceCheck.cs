using UnityEngine;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "DiceCheck", menuName = "DungeonDice/Event Branch/DiceCheck")]
    public class DiceCheck : EventBranch
    {
        [SerializeField] int cutline;

        public override int GetBranch(int value)
        {
            if(value >= cutline) return 0;
            else return 1;
        }
    } 
}