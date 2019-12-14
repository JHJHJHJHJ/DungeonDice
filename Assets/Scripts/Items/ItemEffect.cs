using UnityEngine;

namespace DungeonDice.Items
{
    public abstract class ItemEffect : ScriptableObject
    {
        public abstract void Use();
    } 
}