using UnityEngine;
using DungeonDice.UI;
using DungeonDice.Objects;
using DungeonDice.Core;
using DungeonDice.Characters;
using DungeonDice.Tiles;

public class InputRaycast : MonoBehaviour
{
    DiceUI diceUI;
    TileSelector tileSelector;

    bool enemyWindowIsOpen = false;

    private void Awake()
    {
        diceUI = FindObjectOfType<DiceUI>();
        tileSelector = FindObjectOfType<TileSelector>();
    }

    private void Update()
    {
        GetMouseClick();
        GetTouch();
    }

    void GetMouseClick() // 디버깅을 위함
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                HandleHit(hit);
            }
        }
    }

    void GetTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

                if (hit)
                {
                    HandleHit(hit);
                }
            }
        }
    }

    void HandleHit(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag("EnemyDice"))
        {
            ToggleEnemyDiceWindow();
        }

        if (hit.collider.CompareTag("ShopObject"))
        {
            if (!FindObjectOfType<Player>().isShopping) return;

            FindObjectOfType<ShopManager>().SelectThisItem(hit.collider.gameObject);
        }

        // if (hit.collider.CompareTag("Tile"))
        // {
        //     if (!tileSelector.isSelecting) return;

        //     Tile selectedTile = hit.collider.GetComponent<Tile>();

        //     if(!selectedTile.isSelected) tileSelector.SelectTile(selectedTile);
        //     else tileSelector.UnselectTile(selectedTile);
        // }
    }

    void ToggleEnemyDiceWindow()
    {
        if (!enemyWindowIsOpen)
        {
            diceUI.OpenEnemyDiceDetailWindow();
            enemyWindowIsOpen = true;
        }
        else
        {
            enemyWindowIsOpen = false;
        }
    }
}