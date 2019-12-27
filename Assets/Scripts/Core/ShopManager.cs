using UnityEngine;
using DungeonDice.Objects;
using DungeonDice.Tiles;

namespace DungeonDice.Core
{
    public class ShopManager : MonoBehaviour
    {
        EventManager eventManager;
        [SerializeField] TileEvent selectItemEvent;

        private void Awake()
        {
            eventManager = GetComponent<EventManager>();
        }

        public void SelectThisItem(GameObject itemSelected)
        {
            Ground ground = FindObjectOfType<Ground>();

            int index = 0;
            for (int i = 0; i < ground.shopItemObjects.Length; i++)
            {
                if(ground.shopItemObjects[i] == itemSelected)
                {
                    index = i;
                }
            }
            FindObjectOfType<Shop>().SetCurrentIndex(index);

            eventManager.MoveToNextEvent(selectItemEvent);
        }
    }
}
