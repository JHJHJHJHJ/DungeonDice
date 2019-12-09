using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonDice.Characters;

namespace DungeonDice.Dices
{
    public class DiceRoll : MonoBehaviour
    {
        Dice diceToRoll;

        public bool isRolling = false;
        
        int resultIndex;

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

        public void ActivateDiceEffect() // 애니메이션에서 실행됨
        {
            Side resultSide = diceToRoll.sides[resultIndex]; 
            print(resultSide.name + " " + resultSide.value);

            resultSide.diceEffect.Effect(resultSide.value);

            isRolling = false;
        }
    }
}

