using UnityEngine;

namespace DungeonDice.Tiles
{
    [CreateAssetMenu(fileName = "Event", menuName = "DungeonDice/New TileEvent", order = 0)]
    public class TileEvent : ScriptableObject
    {
        [TextArea]
        public string[] descriptions;
        public TileEvent[] nextEvents;
        public string[] optionLabel;
    }
}
