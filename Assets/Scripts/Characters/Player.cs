using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonDice.Stats;
using DungeonDice.Tiles;

namespace DungeonDice.Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float restTime = 0.3f;

        public bool isMoving = false;
        public int currentTileIndex = 0;
        public Tile currentTile;

        Rigidbody2D myRigidbody;
        Animator animator;
        HP hp;

        TilesContainer tilesContainer;

        private void Awake()
        {
            tilesContainer = FindObjectOfType<TilesContainer>();

            myRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            hp = GetComponent<HP>();
        }

        private void Start()
        {
            UpdateCurrentTile();
        }

        public void UpdateCurrentTile()
        {
            currentTile = tilesContainer.currentTileList[currentTileIndex];
        }

        public IEnumerator Move(int steps)
        {
            isMoving = true;
            animator.SetBool("isMoving", true);

            while (steps > 0)
            {
                int nextTileIndex;
                if (currentTileIndex == tilesContainer.currentTileList.Count - 1)
                {
                    nextTileIndex = 0;
                }
                else
                {
                    nextTileIndex = currentTileIndex + 1;
                }

                Transform targetTile = tilesContainer.currentTileList[nextTileIndex].transform;

                FlipSprite(transform, targetTile);

                while (MoveToNextTile(targetTile)) { yield return null; }

                currentTileIndex = nextTileIndex;
                steps--;

                yield return new WaitForSeconds(restTime);
            }

            isMoving = false;
            animator.SetBool("isMoving", false);
            UpdateCurrentTile();

            FindObjectOfType<StateHolder>().SetPhaseToEvent();
        }

        bool MoveToNextTile(Transform targetTile)
        {
            Vector3 targetPos = new Vector3(targetTile.position.x, targetTile.position.y, targetTile.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);     

            return targetPos != transform.position;
        }

        void FlipSprite(Transform currentTile, Transform targetTile)
        {
            transform.localScale = new Vector2(Mathf.Sign(targetTile.position.x - currentTile.position.x), 1f);
        }
    }
}

