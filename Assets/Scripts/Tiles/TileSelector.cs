using UnityEngine;
using System.Collections.Generic;

namespace DungeonDice.Tiles
{
    public class TileSelector : MonoBehaviour
    {
        public bool isSelecting = false;

        public delegate void DoSomethingToSelectedTile(Tile tile);
        DoSomethingToSelectedTile DoSomething;

        public List<Tile> selectedTiles = new List<Tile>();
        int selectCount = 3;

        GameMessage gameMessage;

        private void Awake()
        {
            gameMessage = FindObjectOfType<GameMessage>();
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
            if (selectedTiles.Count >= selectCount) return;

            selectedTile.isSelected = true;

            foreach (Transform child in selectedTile.transform)
            {
                if (!child.GetComponent<SpriteRenderer>()) continue;

                child.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f, 1f);
            }

            selectedTiles.Add(selectedTile);
            UpdatgeMessage();
        }

        public void UnselectTile(Tile unselectedTile)
        {
            unselectedTile.isSelected = false;

            foreach (Transform child in unselectedTile.transform)
            {
                if (!child.GetComponent<SpriteRenderer>()) continue;

                child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            selectedTiles.Remove(unselectedTile);
            UpdatgeMessage();
        }

        void UpdatgeMessage()
        {
            string message = "타일을 선택하세요.";
            string counting = "(" + selectedTiles.Count.ToString() + "/" + selectCount.ToString() + ")";
            message = message + "\n" + counting;

            gameMessage.SetMessage(message);
        }
    }
}
