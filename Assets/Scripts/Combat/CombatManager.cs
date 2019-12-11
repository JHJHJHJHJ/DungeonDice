using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Dices;
using DungeonDice.Characters;

namespace DungeonDice.Combat
{
    public enum CombatState
    {
        START, STANDBY, PLAYERTURN, ENEMYTURN, WIN
    }

    public class CombatManager : MonoBehaviour
    {
        public CombatState state;

        public IEnumerator InitializeCombat()
        {
            state = CombatState.START;

            yield return null;

            SetupStandbyState();
        }

        void SetupStandbyState()
        {
            state = CombatState.STANDBY;
            FindObjectOfType<StateHolder>().SetPhaseToCombat();
        }

        public IEnumerator DoAction(Side resultSide)
        {
            GameObject target = null;
            if (state == CombatState.PLAYERTURN) target = FindObjectOfType<Enemy>().gameObject;
            else if (state == CombatState.ENEMYTURN) target = FindObjectOfType<Player>().gameObject;

            resultSide.diceEffect.Activate(resultSide.value, target);

            yield return new WaitForSeconds(0.5f);

            if (state == CombatState.PLAYERTURN) StartCoroutine(HandleEnemyTurn());
            else if (state == CombatState.ENEMYTURN) SetupStandbyState();
        }

        IEnumerator HandleEnemyTurn()
        {
            state = CombatState.ENEMYTURN;

            yield return null;
        }
    }
}

