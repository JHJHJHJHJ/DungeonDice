using UnityEngine;
using TMPro;
using DungeonDice.Stats;

namespace DungeonDice.Combat
{
    public class HUDController : MonoBehaviour
    {
        public void UpdateHUDs(GameObject target)
        {
            UpdateShield(target);
        }

        public void UpdateShield(GameObject target)
        {
            Shield shield = target.GetComponent<Shield>();
            CombatHUD combatHUD = target.GetComponent<CombatHUD>();

            if(shield.GetCurrentShield() <= 0)
            {
                combatHUD.shieldText.text = 0.ToString();
                combatHUD.shield.SetActive(false);
            }
            else
            {
                combatHUD.shield.SetActive(true);
                combatHUD.shieldText.text = shield.GetCurrentShield().ToString();
            }
        }
    }
}
