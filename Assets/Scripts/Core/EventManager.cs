using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DungeonDice.Characters;
using DungeonDice.Tiles;
using TMPro;
using DungeonDice.UI;

namespace DungeonDice.Core
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tileName;
        [SerializeField] Button[] buttons;
        [SerializeField] EventTextBox eventTextBox;
        [SerializeField] float timeToType = 0.05f;

        Queue<string> descriptionQueue;
        string currentDescription;
        bool isTyping = false;

        TileEvent currentEvent;
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

        public IEnumerator InitializeTileEvent()
        {
            isActivating = true;

            // levelDirector.SetLevelToEventPhase(player.currentTile.tileInfo.ground, tilesContainer, player);
            tileName.text = player.currentTile.tileInfo.name;
            yield return StartCoroutine(levelDirector.SetLevelToEventPhase(player.currentTile.tileInfo.ground, tilesContainer, player));

            eventTextBox.gameObject.SetActive(true);
            eventTextBox.Open();

            while (eventTextBox.isAnimating)
            {
                yield return null;
            }

            UpdateEvent(player.currentTile.tileInfo.initialTileEvent);
        }

        void UpdateEvent(TileEvent eventToUpdate)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            currentEvent = eventToUpdate;
            descriptionQueue = new Queue<string>();

            foreach (string description in currentEvent.descriptions)
            {
                descriptionQueue.Enqueue(description);
            }
            UpdateDescription();

            nextEvents.Clear();
            for (int i = 0; i < eventToUpdate.nextEvents.Length; i++)
            {
                nextEvents.Add(eventToUpdate.nextEvents[i]);
            }
        }

        void UpdateDescription()
        {
            if (descriptionQueue.Count <= 0) return;

            currentDescription = descriptionQueue.Dequeue();
            StartCoroutine(TypeDescription(currentDescription));
        }

        IEnumerator TypeDescription(string description)
        {
            isTyping = true;

            eventTextBox.descriptionText.text = "";

            foreach (char letter in description.ToCharArray())
            {
                eventTextBox.descriptionText.text += letter;
                yield return new WaitForSeconds(timeToType);
            }

            isTyping = false;
            if (descriptionQueue.Count == 0)
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
            if (isTyping)
            {
                StopAllCoroutines();
                isTyping = false;

                eventTextBox.descriptionText.text = currentDescription;
                if (descriptionQueue.Count == 0)
                {
                    SetOptionButtons(currentEvent);
                }
            }
            else
            {
                if (descriptionQueue == null) return;

                UpdateDescription();
            }
        }

        public void GoToNextEvent0()
        {
            if (nextEvents.Count < 1) StartCoroutine(EndEvent());
            else
            {
                UpdateEvent(nextEvents[0]);
            }
        }

        public void GoToNextEvent1()
        {
            if (nextEvents.Count < 2) StartCoroutine(EndEvent());
            else
            {
                UpdateEvent(nextEvents[1]);
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
            FindObjectOfType<PhaseManager>().SetPhase(Phase.Explore);
        }
    }
}

