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
        [SerializeField] Button[] buttons;
        [SerializeField] EventTextBox eventTextBox;
        [SerializeField] float timeToType = 0.05f;

        string currentDescription;

        [SerializeField] TileEvent currentEvent;
        List<TileEvent> nextEvents = new List<TileEvent>();

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

        IEnumerator HandleEndBattle()
        {
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

        void MoveToNextEvent(TileEvent eventToUpdate)
        {
            UpdateEvent(eventToUpdate);
            HandleEvent();
        }

        void UpdateEvent(TileEvent eventToUpdate)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            currentEvent = eventToUpdate;

            eventTextBox.EnqueueDescriptions(currentEvent.descriptions);

            nextEvents.Clear();
            for (int i = 0; i < eventToUpdate.nextEvents.Length; i++)
            {
                nextEvents.Add(eventToUpdate.nextEvents[i]);
            }
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
                SetOptionButtons(currentEvent);
            }
        }

        void SetOptionButtons(TileEvent eventToUpdate)
        {
            for (int i = 0; i < eventToUpdate.optionLabel.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
                Text optionButtonLabel = buttons[i].transform.GetComponentInChildren<Text>();
                optionButtonLabel.text = eventToUpdate.optionLabel[i];
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
                    SetOptionButtons(currentEvent);
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
            if (nextEvents.Count < i + 1)
            {
                if (FindObjectOfType<Ground>().additionalObject)
                {
                    FindObjectOfType<Ground>().additionalObject.SetActive(false);
                }

                StartCoroutine(EndEvent());
            }
            else
            {
                if (nextEvents[i].CanMove())
                {
                    if (FindObjectOfType<Ground>().additionalObject)
                    {
                        FindObjectOfType<Ground>().additionalObject.SetActive(false);
                    }
                    MoveToNextEvent(nextEvents[i]);
                }
            }
        }

        IEnumerator EndEvent()
        {
            isActivating = false;

            foreach (Button button in buttons)
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

