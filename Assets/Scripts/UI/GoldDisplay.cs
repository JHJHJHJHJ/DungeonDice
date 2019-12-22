using UnityEngine;
using TMPro;
using DungeonDice.Stats;

namespace DungeonDice.UI
{
    public class GoldDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldText;
        [SerializeField] TextMeshProUGUI goldTextShadow;

        Gold gold;

        private void Awake() 
        {
            gold = FindObjectOfType<Gold>();    
        }

        private void Update() 
        {
            UpdateText();
        }

        void UpdateText()
        {
            goldText.text = gold.GetCurrentGold().ToString() + "G";
            goldTextShadow.text = gold.GetCurrentGold().ToString() + "G";
        }
    }
}
