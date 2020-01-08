using UnityEngine;
using System.Collections.Generic;

namespace DungeonDice.Tiles
{
    public class TileSelector : MonoBehaviour
    {
        public bool isSelecting = false;

        public delegate void DoSomethingToSelectedTile(Tile tile);
        DoSomethingToSelectedTile DoSomething;

        List<Tile> selectedTiles = new List<Tile>();
        int selectCount = 3;

        InstructionMessage instructionMessage;

        [SerializeField] GameObject button;

        private void Awake()
        {
            instructionMessage = FindObjectOfType<InstructionMessage>();
        }

        public void ActivateTileSelector(DoSomethingToSelectedTile doSomething, int selectCount)
        {
            isSelecting = true;

            this.selectCount = selectCount;
            DoSomething = new DoSomethingToSelectedTile(doSomething);
            selectedTiles = new List<Tile>();

            UpdatgeMessage();
        }

        public void SelectTile(Tile selectedTile)
        {
            if (selectedTiles.Count >= selectCount)
            {
                UnselectTile(selectedTiles[0]);
            }

            selectedTile.UpdateThisTileSelect(true);

            selectedTiles.Add(selectedTile);
            UpdatgeMessage();

            if (selectedTiles.Count >= selectCount)
            {
                button.SetActive(true);
            }
        }

        public void UnselectTile(Tile unselectedTile)
        {
            button.SetActive(false);

            unselectedTile.UpdateThisTileSelect(false);

            selectedTiles.Remove(unselectedTile);
            UpdatgeMessage();
        }

        void UpdatgeMessage()
        {
            string message = "타일을 선택하세요.";
            string counting = "(" + selectedTiles.Count.ToString() + "/" + selectCount.ToString() + ")";
            message = message + "\n" + counting;

            instructionMessage.SetMessage(message);
        }

        public void EffectDelegate()
        {
            foreach (Tile tile in selectedTiles)
            {
                DoSomething(tile);
            }

            EndTileSelector();
        }

        void EndTileSelector()
        {
            isSelecting = false;
            
            for (int i = 0; i < selectedTiles.Count; i++)
            {
                UnselectTile(selectedTiles[i]);
            }
            
            instructionMessage.SetMessage("");
            button.SetActive(false);
        }
    }
}
