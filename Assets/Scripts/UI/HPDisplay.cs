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

        HP hp;

        private void Awake()
        {
            hp = FindObjectOfType<HP>();
        }

        private void Update()
        {
            UpdateBar();
            UpdateText();
        }

        void UpdateBar()
        {
            float hpRatio = hp.GetCurrentHP() / hp.GetMaxHP();
            hpBar.transform.localScale = new Vector2(hpRatio, 1f);
        }

        void UpdateText()
        {
            hpText.text = hp.GetCurrentHP() + "/" + hp.GetMaxHP();
        }
    }
}
