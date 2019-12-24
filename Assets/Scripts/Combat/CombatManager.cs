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

        CombatTurn turn;
        HUDController hudController;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            turn = GetComponent<CombatTurn>();
            hudController = GetComponent<HUDController>();
        }

        public IEnumerator InitializeCombat()
        {
            state = CombatState.START;

            enemy = FindObjectOfType<Enemy>();
            enemyDice = enemy.dice.gameObject;

            player.GetComponent<CombatHUD>().hudCanvas.SetActive(true);
            enemy.GetComponent<CombatHUD>().hudCanvas.SetActive(true);

            yield return null;

            StartCoroutine(SetupStandbyState());
        }

        IEnumerator SetupStandbyState()
        {
            yield return StartCoroutine(turn.HandleTurnEvents(player.gameObject));

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
            SetDiceTarget(out target, out targetName, resultSide);

            resultSide.diceEffect.Activate(resultSide.value, target);
            combatMessage.text = resultSide.diceEffect.GetCombatMessage(targetName, resultSide.value);

            hudController.UpdateHUDs(target); // 아마 실행 방법 연출 애니메이션 들어가면 수정해야할 듯

            yield return new WaitForSeconds(timeToWait);

            if (state == CombatState.PLAYERTURN)
            {
                if (enemy.GetComponent<HP>().GetCurrentHP() <= 0)
                {
                    StartCoroutine(WinCombat());
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

        IEnumerator WinCombat()
        {
            yield return StartCoroutine(KillEnemy());

            yield return new WaitForSeconds(timeToWait);

            combatMessage.text = "";

            yield return new WaitForSeconds(0.3f);

            yield return StartCoroutine(EndCombat());
        }

        IEnumerator HandleEnemyTurn()
        {
            state = CombatState.ENEMYTURN;

            yield return StartCoroutine(turn.HandleTurnEvents(enemy.gameObject));

            combatMessage.text = enemy.enemyName + "은(는) 주사위를 던졌다!";

            Dice currentEnemyDice = enemy.GetCurrentEnemyDice();
            yield return StartCoroutine(RollEnemyDice(currentEnemyDice));

            enemy.MoveToNextPattern();

            StartCoroutine(SetupStandbyState());
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
            foreach (Transform child in enemy.transform)
            {
                Destroy(child.gameObject);
            }

            combatMessage.text = enemy.enemyName + "을(를) 물리쳤다!";

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

        void SetDiceTarget(out GameObject target, out string targetName, Side resultSide)
        {
            target = null;
            targetName = "";

            if (state == CombatState.PLAYERTURN)
            {        
                playerDice.SetActive(false);

                if (resultSide.diceEffect.isSelf())
                {
                    target = player.gameObject;
                    targetName = "당신";
                }
                else
                {
                    target = enemy.gameObject;
                    targetName = enemy.enemyName;
                }
            }
            else if (state == CombatState.ENEMYTURN)
            {
                enemyDice.SetActive(false);

                if (resultSide.diceEffect.isSelf())
                {
                    target = enemy.gameObject;
                    targetName = enemy.enemyName;
                }
                else
                {
                    target = player.gameObject;
                    targetName = "당신";
                }
            }
        }
    }
}

