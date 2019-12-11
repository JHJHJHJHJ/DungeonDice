using UnityEngine;

namespace DungeonDice.Tiles
{
    public abstract class TileEventEffect : ScriptableObject
    {
        public abstract void Activate();
    } 
}