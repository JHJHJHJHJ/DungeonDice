using UnityEngine;
using System.Collections.Generic;
using DungeonDice.Dices;

namespace DungeonDice.Objects
{
    public class Enemy : MonoBehaviour
    {
        public DiceRoller dice;

        [SerializeField] DicePattern[] dicePatterns;
        int currentOrder = 0;

        [SerializeField] Dice[] enemyDices;

        public int minGold;
        public int maxGold;

        private void Start() 
        {
            currentOrder = 0;
        }

        public Dice GetCurrentEnemyDice()
        {
            return enemyDices[GetCurrentDiceIndex()];
        }

        int GetCurrentDiceIndex()
        {
            int[] indexes = dicePatterns[currentOrder].diceIndexes;

            int i = Random.Range(0, indexes.Length);
            return indexes[i];
        }

        public void MoveToNextPattern()
        {
            currentOrder = (currentOrder + 1) % dicePatterns.Length;
        }

        public void Die()
        {
            GetComponent<SpriteRenderer>().sprite = null;
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    [System.Serializable]
    public class DicePattern
    {
        public int[] diceIndexes;
    }
}
