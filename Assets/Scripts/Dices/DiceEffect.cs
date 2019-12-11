using UnityEngine;

namespace DungeonDice.Dices
{
    public abstract class DiceEffect : ScriptableObject
    {
        public abstract void Activate(int value);
    } 
}