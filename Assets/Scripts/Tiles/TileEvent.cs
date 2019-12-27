using UnityEngine;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "Event", menuName = "DungeonDice/New TileEvent", order = 0)]
    public class TileEvent : ScriptableObject
    {
        [SerializeField] MoveCondition[] moveConditions;

        public TileEventEffect tileEventEffect;
        [TextArea]
        public string[] descriptions;
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
