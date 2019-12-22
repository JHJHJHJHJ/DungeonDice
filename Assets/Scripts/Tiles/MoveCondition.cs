using UnityEngine;

namespace DungeonDice.Tiles
{
    public abstract class MoveCondition : ScriptableObject
    {
        public abstract bool CanMove();
    } 
}