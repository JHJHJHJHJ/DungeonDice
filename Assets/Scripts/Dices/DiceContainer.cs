using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonDice.Dices
{
    public class DiceContainer : MonoBehaviour
    {
        [SerializeField] List<Dice> defaultMoveDices;
        [SerializeField] List<Dice> defaultCombatDices;

        public Dice currentExploreDice;
        public Dice currentCombatDice;

        public List<Dice> exploreDices = new List<Dice>();
        public List<Dice> combatDices = new List<Dice>();

        int countToRoll = 5;
        int currentDiceNum = 1;
        public bool isRolling = false;

        private void Awake()
        {
            InitializeDices();
        }

        private void Start()
        {
            
        }

        void InitializeDices()
        {
            foreach (Dice diceToAdd in defaultMoveDices)
            {
                exploreDices.Add(diceToAdd);
            }
            foreach (Dice diceToAdd in defaultCombatDices)
            {
                combatDices.Add(diceToAdd);
            }

            currentExploreDice = defaultMoveDices[0];
            currentCombatDice = defaultCombatDices[0];
        }

        public int Roll(string which)
        {
            isRolling = true;

            Dice diceToRoll = null;
            if (which == "Move") diceToRoll = currentExploreDice;
            else if (which == "Combat") diceToRoll = currentCombatDice;

            isRolling = false;

            return diceToRoll.sides[Random.Range(0, diceToRoll.sides.Length)].value;
        }
    }
}
