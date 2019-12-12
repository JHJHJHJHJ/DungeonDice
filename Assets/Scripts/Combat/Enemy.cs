using UnityEngine;
using System.Collections.Generic;
using DungeonDice.Dices;

namespace DungeonDice.Combat
{
    public class Enemy : MonoBehaviour
    {
        public DiceRoller dice;

        [SerializeField] DicePattern[] dicePatterns;
        int currentOrder = 0;

        [SerializeField] Dice[] enemyDices;

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
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class DicePattern
    {
        public int[] diceIndexes;
    }
}
