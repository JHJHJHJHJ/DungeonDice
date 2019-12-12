using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Tiles;
using DungeonDice.Characters;
using DungeonDice.UI;

namespace DungeonDice.Core
{
    public class GameMaster : MonoBehaviour
    {
        Player player;
        TilesContainer tilesContainer;
        StateHolder stateHolder;
        DiceUI diceUI;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            tilesContainer = FindObjectOfType<TilesContainer>();
            stateHolder = GetComponent<StateHolder>();
            diceUI = FindObjectOfType<DiceUI>();
        }

        private void Start()
        {
            stateHolder.SetPhaseToExplore += HandleExplorePhase;
            stateHolder.SetPhaseToEvent += HandleTileEventPhase;
            stateHolder.SetPhaseToCombat += HandleCombatPhase;

            stateHolder.SetPhaseToExplore();
            stateHolder.SetFloor(1);
        }

        void HandleExplorePhase()
        {
            stateHolder.SetPhase(Phase.EXPLORE);
            diceUI.SetUIAtPhase(Phase.EXPLORE);
        }

        void HandleTileEventPhase()
        {
            stateHolder.SetPhase(Phase.EVENT);

            diceUI.SetUIAtPhase(Phase.EVENT);

            Tile currentTile = tilesContainer.currentTileList[player.currentTileIndex].GetComponent<Tile>();

            if (currentTile.tileInfo.initialTileEvent != null)
            {
                StartCoroutine(GetComponent<EventManager>().InitializeTileEvent(currentTile));
            }
            else
            {
                stateHolder.SetPhaseToExplore();
            }
        }

        void HandleCombatPhase()
        {
            stateHolder.SetPhase(Phase.COMBAT);
            diceUI.SetUIAtPhase(Phase.COMBAT);
        }
    }
}

