using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonDice.Characters;
using DungeonDice.Tiles;
using TMPro;
using DungeonDice.UI;
using DungeonDice.Combat;

namespace DungeonDice.Core
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tileName;
        [SerializeField] EventButtons eventButtons;
        [SerializeField] EventTextBox eventTextBox;

        string currentDescription;

        TileEvent currentEvent;

        public bool isActivating = false;

        Player player;
        TilesContainer tilesContainer;
        LevelDirector levelDirector;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            tilesContainer = FindObjectOfType<TilesContainer>();
            levelDirector = GetComponent<LevelDirector>();
        }

        private void Start()
        {
            FindObjectOfType<CombatManager>().EndCombat += HandleEndBattle;
        }

        private void Update()
        {
            if(eventButtons.buttons[0].gameObject.activeSelf)
            {
                UpdateOptionButtons(currentEvent);
            }            
        }

        IEnumerator HandleEndBattle()
        {
            player.GetComponent<CombatHUD>().hudCanvas.SetActive(false);

            player.currentTile.DestroyTileObject();

            yield return StartCoroutine(OpenEventTextBox());

            MoveToNextEvent(FindObjectOfType<CombatManager>().winTileEvent);
        }

        public IEnumerator InitializeTileEvent(Tile tileToInitialize)
        {
            yield return StartCoroutine(levelDirector.SetLevelToEventPhase(tileToInitialize.tileInfo.ground, tilesContainer, player));

            isActivating = true;
            tileName.text = tileToInitialize.tileInfo.name;

            yield return StartCoroutine(OpenEventTextBox());

            MoveToNextEvent(tileToInitialize.tileInfo.initialTileEvent);
        }

        IEnumerator OpenEventTextBox()
        {
            eventTextBox.gameObject.SetActive(true);
            eventTextBox.Open();

            while (eventTextBox.isAnimating)
            {
                yield return null;
            }
        }

        public void MoveToNextEvent(TileEvent eventToUpdate)
        {
            StopAllCoroutines();

            UpdateEvent(eventToUpdate);
            HandleEvent();
        }

        void UpdateEvent(TileEvent eventToUpdate)
        {
            for (int i = 0; i < eventButtons.buttons.Length; i++)
            {
                eventButtons.buttons[i].gameObject.SetActive(false);
            }

            currentEvent = eventToUpdate;

            eventTextBox.EnqueueDescriptions(currentEvent.descriptions);
        }

        void HandleEvent()
        {
            if (currentEvent.tileEventEffect != null)
            {
                currentEvent.tileEventEffect.Activate();
            }

            StartCoroutine(UpdateDescription());
        }

        IEnumerator UpdateDescription()
        {
            if (eventTextBox.descriptionQueue.Count <= 0) yield break;

            currentDescription = eventTextBox.descriptionQueue.Dequeue();
            yield return StartCoroutine(eventTextBox.TypeDescription(currentDescription));

            if (eventTextBox.descriptionQueue.Count == 0)
            {
                ShowOptionButtons();
            }
        }

        public void ShowOptionButtons()
        {
            for (int i = 0; i < currentEvent.options.Length; i++)
            {
                if(currentEvent.options[i].label == "")
                {
                    continue;
                }

                eventButtons.buttons[i].gameObject.SetActive(true);
                UpdateOptionButtons(currentEvent);
            }
        }

        public void UpdateOptionButtons(TileEvent eventToUpdate)
        {
            for (int i = 0; i < eventToUpdate.options.Length; i++)
            {
                Text optionButtonLabel = eventButtons.buttons[i].transform.GetComponentInChildren<Text>();
                optionButtonLabel.text = eventToUpdate.options[i].label;

                eventButtons.buttons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                optionButtonLabel.color = new Color(1f, 1f, 1f, 1f);

                if (eventToUpdate.options[i].nextEvent == null) continue;

                if (!eventToUpdate.options[i].nextEvent.CanMove())
                {
                    eventButtons.buttons[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    optionButtonLabel.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
            }
        }

        public void ClickToSkipOrNext()
        {
            if (eventTextBox.isTyping)
            {
                StopAllCoroutines();
                eventTextBox.isTyping = false;

                eventTextBox.descriptionText.text = currentDescription;
                if (eventTextBox.descriptionQueue.Count == 0)
                {
                    ShowOptionButtons();
                }
            }
            else
            {
                if (eventTextBox.descriptionQueue == null) return;

                StartCoroutine(UpdateDescription());
            }
        }

        public void GoToNextEvent(int i)
        {
            if (currentEvent.options[i].nextEvent == null)
            {
                if (FindObjectOfType<Ground>().additionalObject)
                {
                    FindObjectOfType<Ground>().additionalObject.SetActive(false);
                }

                StartCoroutine(EndEvent());
            }
            else
            {
                if (currentEvent.options[i].nextEvent.CanMove())
                {
                    if (FindObjectOfType<Ground>().additionalObject)
                    {
                        FindObjectOfType<Ground>().additionalObject.SetActive(false);
                    }
                    MoveToNextEvent(currentEvent.options[i].nextEvent);
                }
            }
        }

        IEnumerator EndEvent()
        {
            isActivating = false;

            foreach (Button button in eventButtons.buttons)
            {
                button.gameObject.SetActive(false);
            }

            eventTextBox.Close();
            while (eventTextBox.isAnimating)
            {
                yield return null;
            }

            tileName.text = "";

            yield return StartCoroutine(levelDirector.SetLevelToExplorePhase(FindObjectOfType<Ground>(), tilesContainer, player));

            FindObjectOfType<StateHolder>().SetPhaseToExplore();
        }
    }
}

