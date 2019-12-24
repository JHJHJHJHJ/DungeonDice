using UnityEngine;
using DungeonDice.UI;
using DungeonDice.Objects;

public class InputRaycast : MonoBehaviour
{
    DiceUI diceUI;

    bool enemyWindowIsOpen = false;

    private void Awake()
    {
        diceUI = FindObjectOfType<DiceUI>();
    }

    private void Update()
    {
        GetMouseClick();
        GetTouch();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                if (hit.collider.CompareTag("EnemyDice"))
                {
                    ToggleEnemyDiceWindow();
                }
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
                    if (hit.collider.CompareTag("EnemyDice"))
                    {
                        ToggleEnemyDiceWindow();
                    }
                }
            }
        }
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