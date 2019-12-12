using UnityEngine;
using UnityEngine.UI;
using DungeonDice.Dices;
using TMPro;
using DungeonDice.Characters;
using DungeonDice.Combat;
using System.Collections;
using DungeonDice.Stats;

namespace DungeonDice.UI
{
    public class DiceUI : MonoBehaviour
    {
        [Header("Control UI")]
        [SerializeField] Image currentExploreDiceImage;
        [SerializeField] Image currentCombatDiceImage;
        [SerializeField] GameObject exploreRollButton;
        [SerializeField] GameObject combatRollButton;
        [SerializeField] GameObject playerDice;

        [Header("Explore Dice Detail Window")]
        [SerializeField] GameObject exploreDiceDetailWindow;
        [SerializeField] Image[] exploreDiceDetailWindowSides;
        [SerializeField] TextMeshProUGUI exploreNameText;
        [SerializeField] TextMeshProUGUI exploreDescriptionText;

        [Header("Combat Dice Detail Window")]
        [SerializeField] GameObject combatDiceDetailWindow;
        [SerializeField] Image[] combatDiceDetailWindowSides;
        [SerializeField] TextMeshProUGUI combatNameText;
        [SerializeField] TextMeshProUGUI combatDescriptionText;

        bool exploreWindowIsOpen = false;
        bool combatWindowIsOpen = false;

        int exploreDiceIndex = 0;
        int combatDiceIndex = 0;

        DiceContainer diceContainer;

        private void Awake()
        {
            diceContainer = FindObjectOfType<DiceContainer>();
        }

        private void Start()
        {
            UpdateDiceImages();
        }

        void UpdateDiceImages()
        {
            currentExploreDiceImage.sprite = diceContainer.currentExploreDice.repSprite;
            currentCombatDiceImage.sprite = diceContainer.currentCombatDice.repSprite;
        }

        public void ChangeExploreDiceToLeft()
        {
            if(!CanClick()) return;

            if (exploreDiceIndex <= 0)
            {
                exploreDiceIndex = diceContainer.exploreDices.Count - 1;
            }
            else
            {
                exploreDiceIndex--;
            }

            diceContainer.currentExploreDice = diceContainer.exploreDices[exploreDiceIndex];

            UpdateDiceImages();
            UpdateSideImages();
            UpdateSideInfo(0);
        }

        public void ChangeExploreDiceToRight()
        {
            if(!CanClick()) return;

            if (exploreDiceIndex >= diceContainer.exploreDices.Count - 1)
            {
                exploreDiceIndex = 0;
            }
            else
            {
                exploreDiceIndex++;
            }

            diceContainer.currentExploreDice = diceContainer.exploreDices[exploreDiceIndex];

            UpdateDiceImages();
            UpdateSideImages();
            UpdateSideInfo(0);
        }

        public void ChangeCombatDiceToLeft()
        {
            if(!CanClick()) return;

            if (combatDiceIndex <= 0)
            {
                combatDiceIndex = diceContainer.combatDices.Count - 1;
            }
            else
            {
                combatDiceIndex--;
            }

            diceContainer.currentCombatDice = diceContainer.combatDices[combatDiceIndex];

            UpdateDiceImages();
            UpdateSideImages();
            UpdateSideInfo(0);
        }

        public void ChangeCombatDiceToRight()
        {
            if(!CanClick()) return;

            if (combatDiceIndex >= diceContainer.combatDices.Count - 1)
            {
                combatDiceIndex = 0;
            }
            else
            {
                combatDiceIndex++;
            }

            diceContainer.currentCombatDice = diceContainer.combatDices[combatDiceIndex];

            UpdateDiceImages();
            UpdateSideImages();
            UpdateSideInfo(0);
        }

        public void ToggleExploreDiceDetailWindow()
        {
            if(!CanClick()) return;

            combatDiceDetailWindow.SetActive(false);
            combatWindowIsOpen = false;

            if (exploreWindowIsOpen)
            {
                exploreDiceDetailWindow.SetActive(false);
                exploreWindowIsOpen = false;
            }
            else
            {
                exploreDiceDetailWindow.SetActive(true);
                exploreWindowIsOpen = true;

                UpdateSideImages();
                UpdateSideInfo(0);
            }
        }

        public void ToggleCombatDiceDetailWindow()
        {
            if(!CanClick()) return;

            exploreDiceDetailWindow.SetActive(false);
            exploreWindowIsOpen = false;

            if (combatWindowIsOpen)
            {
                combatDiceDetailWindow.SetActive(false);
                combatWindowIsOpen = false;
            }
            else
            {
                combatDiceDetailWindow.SetActive(true);
                combatWindowIsOpen = true;

                UpdateSideImages();
                UpdateSideInfo(0);
            }
        }

        void UpdateSideImages()
        {
            if (exploreWindowIsOpen)
            {
                foreach (Image image in exploreDiceDetailWindowSides)
                {
                    image.gameObject.SetActive(false);
                }

                Side[] currentDiceSides = diceContainer.currentExploreDice.sides;

                for (int i = 0; i < currentDiceSides.Length; i++)
                {
                    exploreDiceDetailWindowSides[i].gameObject.SetActive(true);
                    exploreDiceDetailWindowSides[i].sprite = currentDiceSides[i].sideSprite;
                }
            }

            if (combatWindowIsOpen)
            {
                foreach (Image image in combatDiceDetailWindowSides)
                {
                    image.gameObject.SetActive(false);
                }

                Side[] currentDiceSides = diceContainer.currentCombatDice.sides;

                for (int i = 0; i < currentDiceSides.Length; i++)
                {
                    combatDiceDetailWindowSides[i].gameObject.SetActive(true);
                    combatDiceDetailWindowSides[i].sprite = currentDiceSides[i].sideSprite;
                }
            }
        }

        public void UpdateSideInfo(int i)
        {
            if (exploreWindowIsOpen)
            {
                Side[] currentDiceSides = diceContainer.currentExploreDice.sides;

                exploreNameText.text = currentDiceSides[i].sideName + " " + currentDiceSides[i].value;
                exploreDescriptionText.text = currentDiceSides[i].description;
            }

            if (combatWindowIsOpen)
            {
                Side[] currentDiceSides = diceContainer.currentCombatDice.sides;

                combatNameText.text = currentDiceSides[i].sideName + " " + currentDiceSides[i].value;
                combatDescriptionText.text = currentDiceSides[i].description;
            }
        }

        public void ShutWindows()
        {
            exploreDiceDetailWindow.SetActive(false);
            combatDiceDetailWindow.SetActive(false);

            exploreWindowIsOpen = false;
            combatWindowIsOpen = false;
        }

        public void RollCurrentDice(string diceType) // 굴린다 버튼. explore : combat
        {
            Dice diceToRoll = diceContainer.currentExploreDice;
            Image currentDiceUIImage = currentExploreDiceImage;

            if (diceType == "combat")
            {
                diceToRoll = diceContainer.currentCombatDice;
                currentDiceUIImage = currentCombatDiceImage;
                FindObjectOfType<CombatManager>().state = CombatState.PLAYERTURN;
            }
            else
            {
                FindObjectOfType<Player>().GetComponent<FP>().DealFP(-1f);
            }

            ShutWindows();
            exploreRollButton.SetActive(false);
            combatRollButton.SetActive(false);

            currentDiceUIImage.color = new Color32(255, 255, 255, 127);
            currentDiceUIImage.GetComponent<Button>().enabled = false;

            StartCoroutine(Roll(diceToRoll));
        }

        IEnumerator Roll(Dice diceToRoll)
        {
            playerDice.SetActive(true);
            DiceRoller diceRoller = playerDice.GetComponent<DiceRoller>();
            diceRoller.TriggerDiceRoll(diceToRoll);

            while(diceRoller.isRolling)
            {
                yield return null;
            }

            Side resultSide = diceToRoll.sides[diceRoller.resultIndex];

            if (FindObjectOfType<StateHolder>().GetCurrentPhase() == Phase.COMBAT)
            {
                yield return StartCoroutine(FindObjectOfType<CombatManager>().DoAction(resultSide));
            }
            else
            {
                resultSide.diceEffect.Activate(resultSide.value, null);
            }
        }

        public void SetUIAtPhase(Phase phase)
        {
            playerDice.SetActive(false);

            exploreRollButton.SetActive(false);
            combatRollButton.SetActive(false);

            currentExploreDiceImage.color = new Color32(255, 255, 255, 255);
            currentExploreDiceImage.GetComponent<Button>().enabled = true;
            currentCombatDiceImage.color = new Color32(255, 255, 255, 255);
            currentCombatDiceImage.GetComponent<Button>().enabled = true;

            if (phase == Phase.EXPLORE)
            {
                exploreRollButton.SetActive(true);
            }
            else if(phase == Phase.COMBAT)
            {
                combatRollButton.SetActive(true);
            }
        }

        bool CanClick()
        {
            if(FindObjectOfType<StateHolder>().GetCurrentPhase() == Phase.COMBAT)
            {
                if(FindObjectOfType<CombatManager>().state != CombatState.STANDBY)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
