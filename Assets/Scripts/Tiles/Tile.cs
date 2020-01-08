using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonDice.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileInfo tileInfo;

        [Header("Tile State")]
        public int index = 0;
        public bool isHidden = false;
        public float spriteColor = 1f;

        [Header("Tile Event")]
        public TileEvent initialTileEvent0;
        public bool isChanged = false;
        public TileEvent initialTileEvent1;

        [HideInInspector]
        public bool isSelected = false;
        [SerializeField] SpriteRenderer outline;

        Transform tileObject = null;

        public void SetUpTile()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.CompareTag("TileObject"))
                {
                    tileObject = child;
                }
            }
        }

        public void Hide()
        {
            isHidden = true;
            spriteColor = 0.4f;

            if (tileObject != null)
            {
                tileObject.gameObject.SetActive(false);
            }

            foreach (Transform child in transform)
            {
                if (!child.GetComponent<SpriteRenderer>() || child.CompareTag("Outline")) return;

                child.GetComponent<SpriteRenderer>().color = new Color(spriteColor, spriteColor, spriteColor, 1f);
            }
        }

        public void Open()
        {
            isHidden = false;
            spriteColor = 1f;

            if (tileObject != null)
            {
                tileObject.gameObject.SetActive(true);
            }

            foreach (Transform child in transform)
            {
                if (!child.GetComponent<SpriteRenderer>() || child.CompareTag("Outline")) continue;

                child.GetComponent<SpriteRenderer>().color = new Color(spriteColor, spriteColor, spriteColor, 1f);
            }
        }

        public void DestroyTileObject()
        {
            Destroy(tileObject.gameObject);
            initialTileEvent0 = null;
        }

        public void Change()
        {
            isChanged = true;
        }

        public void UpdateThisTileSelect(bool isSelected)
        {
            this.isSelected = isSelected;
            outline.gameObject.SetActive(isSelected);
        }
    }

    [System.Serializable]
    public class TileInfo
    {
        public TileType tileType;
        public int number;
        public string name;
        public Ground ground;
        [TextArea]
        public string description;
    }
}

