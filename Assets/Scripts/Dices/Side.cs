using UnityEngine;

namespace DungeonDice.Dices
{
    [CreateAssetMenu(fileName = "Side", menuName = "DungeonDice/New Side", order = 0)]
    public class Side : ScriptableObject
    {
        public Sprite sideSprite;
        public int value;
        public string sideName;
        [TextArea] public string description;
        public DiceEffect diceEffect;
    }
}
