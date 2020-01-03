using UnityEngine;
using DungeonDice.Dices;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "Event", menuName = "DungeonDice/Tile Event", order = 0)]
    public class TileEvent : ScriptableObject
    {
        [Header("Move Condition")]
        [SerializeField] MoveCondition[] moveConditions;

        [Header("Branch")]
        [Tooltip("explore / combat")] public DiceType diceCheck;
        public EventBranch eventBranch;
        
        [Header("Event Info")]
        public TileEventEffect tileEventEffect;
        [TextArea]
        public string[] descriptions;

        [Header("Next Event")]
        public Option[] options;

        public bool CanMove()
        {
            if (moveConditions.Length == 0)
            {
                return true;
            }
            else
            {
                bool canMove = true;

                foreach(MoveCondition moveCondition in moveConditions)
                {
                    if(!moveCondition.CanMove()) canMove = false;
                }

                return canMove;
            }         
        }
    }

    [System.Serializable]
    public class Option
    {
        public string label;
        public TileEvent nextEvent;
    }
}
