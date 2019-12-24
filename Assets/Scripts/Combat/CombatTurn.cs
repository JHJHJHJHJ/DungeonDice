using UnityEngine;
using System.Collections;
using DungeonDice.Characters;
using DungeonDice.Objects;
using DungeonDice.Stats;

namespace DungeonDice.Combat
{
    public class CombatTurn : MonoBehaviour
    {
        int turn = 0;

        public int Get()
        {
            return turn;
        }

        public void MoveToNextTurn()
        {
            turn++;
        }

        public IEnumerator HandleTurnEvents(GameObject target)
        {
            yield return null;

            DiscardShield(target);
        }

        void DiscardShield(GameObject target)
        {
            target.GetComponent<Shield>().InitializeShield();
            FindObjectOfType<HUDController>().UpdateShield(target);
        }
    }
}
