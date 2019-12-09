using UnityEngine;

namespace DungeonDice.Stats
{
    public class HP : MonoBehaviour
    {
        [SerializeField] float initialHP = 3f;

        float maxHP;
        float currentHP;

        private void Start()
        {
            maxHP = initialHP;
            currentHP = maxHP;
        }

        public void DealHP(float deal)
        {
            currentHP = Mathf.Clamp(currentHP + deal, 0f, maxHP);
        }

        public float GetCurrentHP()
        {
            return currentHP;
        }

        public float GetMaxHP()
        {
            return maxHP;
        }
    }
}
