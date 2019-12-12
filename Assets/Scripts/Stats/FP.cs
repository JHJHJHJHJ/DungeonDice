using UnityEngine;

namespace DungeonDice.Stats
{
    public class FP : MonoBehaviour
    {
        [SerializeField] float initialFP = 30f;

        float maxFP;
        float currentFP;

        private void Start()
        {
            maxFP = initialFP;
            currentFP = maxFP;
        }

        public void DealFP(float deal)
        {
            if (currentFP == 0)
            {
                GetComponent<HP>().DealHP(-1f);
            }
            else
            {
                currentFP = Mathf.Clamp(currentFP + deal, 0f, maxFP);
            }
        }

        public float GetCurrentFP()
        {
            return currentFP;
        }

        public float GetMaxFP()
        {
            return maxFP;
        }
    }
}
