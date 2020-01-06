using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Characters;

namespace DungeonDice.Dices
{
    public class DiceRoller : MonoBehaviour // DiceRoller로 이름 바꾸자
    {
        Dice diceToRoll;

        public bool isRolling = false;
        public int resultIndex;

        public void TriggerDiceRoll(Dice diceToRoll)
        {
            isRolling = true;

            this.diceToRoll = diceToRoll;
            GetComponent<SpriteRenderer>().sprite = diceToRoll.repSprite;
            GetComponent<Animator>().SetTrigger("Roll");
        }

        public void RandomizeDice() // 애니메이션에서 실행됨
        {
            int randomIndex = Random.Range(0, diceToRoll.sides.Length);
            resultIndex = randomIndex;

            GetComponent<SpriteRenderer>().sprite = diceToRoll.sides[resultIndex].sideSprite;
        }

        public void RandomizeResultDice() // 애니메이션에서 실행됨
        {
            int randomIndex = 0;
            if(FindObjectOfType<Player>().foresee != 0)
            {
                randomIndex = FindObjectOfType<Player>().foresee - 1;
                FindObjectOfType<Player>().SetForesee(0);
            }
            else
            {
                randomIndex = Random.Range(0, diceToRoll.sides.Length);
            }

            resultIndex = randomIndex;

            GetComponent<SpriteRenderer>().sprite = diceToRoll.sides[resultIndex].sideSprite;
        }

        public void DiceRollEnded() // 애니메이션에서 실행됨
        {
            isRolling = false;
        }
    }
}

