using UnityEngine;

namespace DungeonDice.Tiles
{
    public abstract class EventBranch : ScriptableObject
    {
        public abstract int GetBranch(int value); // return 0 <- true.
    } 
}