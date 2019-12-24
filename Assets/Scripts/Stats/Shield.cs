using UnityEngine;

namespace DungeonDice.Stats
{
    public class Shield : MonoBehaviour
    {
        float currentShield = 0f;

        public void AddShield(float value)
        {
            currentShield += value;
        }

        public void InitializeShield()
        {
            currentShield = 0f;
        }

        public float DealShieldAndGetExtra(float damage)
        {
            float remainder = currentShield - damage;

            currentShield = Mathf.Clamp(remainder, 0, Mathf.Infinity);

            if(remainder < 0)
            {
                return -remainder;
            }
            return 0f;
        }

        public float GetCurrentShield()
        {
            return currentShield;
        }
    }
}
