using UnityEngine;
using System.Collections.Generic;
using DungeonDice.Dices;

namespace DungeonDice.Objects
{
    public class Enemy : MonoBehaviour
    {
        public string enemyName;

        public DiceRoller dice;

        [SerializeField] DicePattern[] dicePatterns;
        int currentOrder = 0;

        [SerializeField] EnemyDice[] enemyDices;

        public int minGold;
        public int maxGold;

        private void Start() 
        {
            currentOrder = 0;
        }

        public Dice GetCurrentEnemyDice()
        {
            return enemyDices[GetCurrentDiceIndex()].dice;
        }

        public string GetCurrentDiceDescription()
        {
            return enemyName + enemyDices[GetCurrentDiceIndex()].description;
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
    }

    [System.Serializable]
    public class DicePattern
    {
        public int[] diceIndexes;
    }

    [System.Serializable]
    public class EnemyDice
    {
        public Dice dice;
        [TextArea] public string description;
    }
}
