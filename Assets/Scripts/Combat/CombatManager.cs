using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Dices;
using DungeonDice.Characters;
using DungeonDice.Stats;
using DungeonDice.Tiles;
using DungeonDice.Objects;
using TMPro;

namespace DungeonDice.Combat
{
    public enum CombatState
    {
        START, STANDBY, PLAYERTURN, ENEMYTURN, WIN
    }

    public class CombatManager : MonoBehaviour
    {
        public CombatState state;
        [SerializeField] TextMeshProUGUI combatMessage;
        [SerializeField] float timeToWait = 0.7f;

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

            combatMessage.text = enemy.GetCurrentDiceDescription(); 
        }

        public void SetupPlayerTurnState()
        {
            state = CombatState.PLAYERTURN;
            enemyDice.SetActive(false);

            combatMessage.text = "당신은 주사위를 던졌다!";
        }

        public IEnumerator DoAction(Side resultSide)
        {
            combatMessage.text = resultSide.sideName + " " + resultSide.value + "!";

            yield return new WaitForSeconds(timeToWait);

            GameObject target = null;
            string targetName = "";
            if (state == CombatState.PLAYERTURN)
            {
                target = FindObjectOfType<Enemy>().gameObject;
                targetName = enemy.enemyName;
                playerDice.SetActive(false);
            }
            else if (state == CombatState.ENEMYTURN)
            {
                target = FindObjectOfType<Player>().gameObject;
                targetName = "당신";
                enemyDice.SetActive(false);
            }

            resultSide.diceEffect.Activate(resultSide.value, target);
            combatMessage.text = resultSide.diceEffect.GetCombatMessage(targetName, resultSide.value);

            yield return new WaitForSeconds(timeToWait);

            if (state == CombatState.PLAYERTURN)
            {
                if (enemy.GetComponent<HP>().GetCurrentHP() <= 0)
                {
                    yield return StartCoroutine(KillEnemy());
                    
                    yield return new WaitForSeconds(timeToWait);

                    combatMessage.text = "";

                    yield return new WaitForSeconds(timeToWait);

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

            combatMessage.text = enemy.enemyName + "은(는) 주사위를 던졌다!";

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

        IEnumerator KillEnemy()
        {
            foreach(Transform child in enemy.transform)
            {
                Destroy(child.gameObject);
            }
            combatMessage.text = enemy.enemyName + "을(를) 쓰려뜨렸다!";
            yield return FadeOutEnemy();
        }

        IEnumerator FadeOutEnemy()
        {
            float alpha = 1f;

            while (alpha >= 0f)
            {
                enemy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);

                alpha -= Time.deltaTime / timeToWait;

                yield return null;
            }
            enemy.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}

