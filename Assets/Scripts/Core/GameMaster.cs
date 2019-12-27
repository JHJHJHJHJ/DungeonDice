using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Tiles;
using DungeonDice.Characters;
using DungeonDice.UI;
using DungeonDice.Stats;
using UnityEngine.SceneManagement;

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
            stateHolder = FindObjectOfType<StateHolder>();
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

        private void Update() 
        {
            if(player.GetComponent<HP>().GetCurrentHP() == 0)    
            {
                GameOver();
            }
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

            currentTile.Open();

            if (currentTile.initialTileEvent0 != null)
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

        void GameOver()
        {
            SceneManager.LoadScene(0); // GOTO 제대로된 연출 만들기 / 상황 별 실행으로 위치 수정하기
        }
    }
}

