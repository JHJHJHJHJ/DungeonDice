using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.Characters;

namespace DungeonDice.Items
{
    [CreateAssetMenu(fileName = "HealFP", menuName = "DungeonDice/Item Effect/HealFP")]
    public class HealFP : ItemEffect
    {
        [SerializeField] int value;

        public override void Use()
        {
            FindObjectOfType<Player>().GetComponent<FP>().DealFP(value);
        }
    }
}