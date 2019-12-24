using UnityEngine;

namespace DungeonDice.Dices
{
    public abstract class DiceEffect : ScriptableObject
    {
        public abstract void Activate(int value, GameObject target);
        
        public abstract bool isSelf(); // 전투 주사위만 사용
        public abstract string GetCombatMessage(string target, int value); // 전투 주사위만 사용
    } 
}