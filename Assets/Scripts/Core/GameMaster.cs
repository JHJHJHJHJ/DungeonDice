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
        PhaseManager phaseManager;
        DiceUI diceUI;

        private void Awake() 
        {
            player = FindObjectOfType<Player>();
            tilesContainer = FindObjectOfType<TilesContainer>();    
            phaseManager = GetComponent<PhaseManager>();
            diceUI = FindObjectOfType<DiceUI>();
        }

        private void Start()
        {
            phaseManager.SetPhase(Phase.Explore);
            phaseManager.SetFloor(1);
        }

        void Update()
        {
            if(phaseManager.GetCurrentPhase() == Phase.Explore && !player.isMoving)
            {
                diceUI.SetUIAtPhase(Phase.Explore);

                phaseManager.SetPhase(Phase.etc);
            }

            if(phaseManager.GetCurrentPhase() == Phase.TileEvent && !player.isMoving)
            {
                diceUI.SetUIAtPhase(Phase.TileEvent);

                Tile currentTile = tilesContainer.currentTileList[player.currentTileIndex].GetComponent<Tile>();

                if(currentTile.tileInfo.initialTileEvent != null)
                {
                    StartCoroutine(GetComponent<EventManager>().InitializeTileEvent());
                    phaseManager.SetPhase(Phase.etc);
                }
                else
                {
                    phaseManager.SetPhase(Phase.Explore);
                }                           
            }
        }
    }
}

