using UnityEngine;

namespace DungeonDice.Stats
{
    public class Gold : MonoBehaviour
    {
        [SerializeField] int initialGold = 30;

        int currentGold;

        private void Start()
        {
            currentGold = initialGold;
        }

        public void GetGold(int gold)
        {
            currentGold += gold;
        }

        public bool SpendGold(int gold)
        {
            bool canSpend = ( currentGold >= gold );

            if(canSpend)
            {
                currentGold -= gold;
            }

            return canSpend;
        }

        public int GetCurrentGold()
        {
            return currentGold;
        }
    }
}
