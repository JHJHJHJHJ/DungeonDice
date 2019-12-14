using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.Characters;

namespace DungeonDice.Items
{
    [CreateAssetMenu(fileName = "HealHP", menuName = "DungeonDice/Item Effect/HealHP")]
    public class HealHP : ItemEffect
    {
        [SerializeField] int value;

        public override void Use()
        {
            FindObjectOfType<Player>().GetComponent<HP>().DealHP(value);
        }
    }
}