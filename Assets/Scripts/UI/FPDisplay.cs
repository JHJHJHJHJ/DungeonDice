using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DungeonDice.Stats;

namespace DungeonDice.UI
{
    public class FPDisplay : MonoBehaviour
    {
        [SerializeField] GameObject fpBar;
        [SerializeField] TextMeshProUGUI fpText;

        FP fp;

        private void Awake()
        {
            fp = FindObjectOfType<FP>();
        }

        private void Update()
        {
            UpdateBar();
            UpdateText();
        }

        void UpdateBar()
        {
            float fpRatio = fp.GetCurrentFP() / fp.GetMaxFP();
            fpBar.transform.localScale = new Vector2(fpRatio, 1f);
        }

        void UpdateText()
        {
            fpText.text = fp.GetCurrentFP() + "/" + fp.GetMaxFP();
        }
    }
}
