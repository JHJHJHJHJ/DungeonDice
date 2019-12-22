using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Dices;
using DungeonDice.Characters;
using DungeonDice.Stats;
using DungeonDice.Tiles;
using DungeonDice.Objects;

namespace DungeonDice.Combat
{
    public enum CombatState
    {
        START, STANDBY, PLAYERTURN, ENEMYTURN, WIN
    }

    public class CombatManager : MonoBehaviour
    {
        public CombatState state;

        public delegate IEnumerator EndCombatDelegate();
        public EndCombatDelegate EndCombat;

        [SerializeField] GameObject playerDice;

        Player player;

        Enemy enemy;
        [HideInInspector]
        public GameObject enemyDice;

        public TileEvent winTileEvent;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
        }

        public IEnumerator InitializeCombat()
        {
            state = CombatState.START;

            enemy = FindObjectOfType<Enemy>();

            yield return null;

            foreach (Transform child in enemy.transform)
            {
                child.gameObject.SetActive(true);
            }
            enemyDice = enemy.dice.gameObject;
            SetupStandbyState();
        }

        void SetupStandbyState()
        {
            state = CombatState.STANDBY;
            FindObjectOfType<StateHolder>().SetPhaseToCombat();

            enemyDice.gameObject.SetActive(true);
            enemyDice.GetComponent<SpriteRenderer>().sprite = enemy.GetCurrentEnemyDice().repSprite;
        }

        public void SetupPlayerTurnState()
        {
            state = CombatState.PLAYERTURN;
            enemyDice.SetActive(false);
        }

        public IEnumerator DoAction(Side resultSide)
        {
            GameObject target = null;
            if (state == CombatState.PLAYERTURN)
            {
                target = FindObjectOfType<Enemy>().gameObject;
                playerDice.SetActive(false);
            }
            else if (state == CombatState.ENEMYTURN)
            {
                target = FindObjectOfType<Player>().gameObject;
                enemyDice.SetActive(false);
            }

            yield return new WaitForSeconds(0.5f);

            resultSide.diceEffect.Activate(resultSide.value, target);

            if (state == CombatState.PLAYERTURN)
            {
                if (enemy.GetComponent<HP>().GetCurrentHP() <= 0)
                {
                    enemy.Die();
                    yield return StartCoroutine(EndCombat());
                }
                else
                {
                    StartCoroutine(HandleEnemyTurn());
                }
            }
            else if (state == CombatState.ENEMYTURN)
            {
                SetupStandbyState();
            }
        }

        IEnumerator HandleEnemyTurn()
        {
            state = CombatState.ENEMYTURN;

            Dice currentEnemyDice = enemy.GetCurrentEnemyDice();
            yield return StartCoroutine(RollEnemyDice(currentEnemyDice));

            enemy.MoveToNextPattern();

            SetupStandbyState();
        }

        IEnumerator RollEnemyDice(Dice diceToRoll)
        {
            enemyDice.SetActive(true);
            DiceRoller diceRoller = enemyDice.GetComponent<DiceRoller>();
            diceRoller.TriggerDiceRoll(diceToRoll);

            while (diceRoller.isRolling)
            {
                yield return null;
            }

            Side resultSide = diceToRoll.sides[diceRoller.resultIndex];

            yield return StartCoroutine(DoAction(resultSide));
        }
    }
}

