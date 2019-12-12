using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DungeonDice.Stats;

namespace DungeonDice.UI
{
    public class HPDisplay : MonoBehaviour
    {
        [SerializeField] GameObject hpBar;
        [SerializeField] TextMeshProUGUI hpText;
        [SerializeField] HP hp;

        private void Update()
        {
            UpdateBar();
            UpdateText();
        }

        void UpdateBar()
        {
            if(hpBar == null) return;

            float hpRatio = hp.GetCurrentHP() / hp.GetMaxHP();
            hpBar.transform.localScale = new Vector2(hpRatio, 1f);
        }

        void UpdateText()
        {
            hpText.text = hp.GetCurrentHP() + "/" + hp.GetMaxHP();
        }
    }
}
