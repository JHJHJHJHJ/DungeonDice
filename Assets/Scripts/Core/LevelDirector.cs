using UnityEngine;
using System.Collections;
using DungeonDice.Tiles;
using DungeonDice.Characters;
using TMPro;

namespace DungeonDice.Core
{
    public class LevelDirector : MonoBehaviour
    {
        [SerializeField] float timeToFade = 0.5f;
        [SerializeField] float tileTrasparentRate = 0.2f;
        [SerializeField] float yOffset = 0.4f;

        float previousPlayerLocalScaleX = 1f;

        public IEnumerator SetLevelToEventPhase(Ground groundToInstantiate, TilesContainer tilesContainer, Player player)
        {
            StartCoroutine(FadeOutPlayer(player, player.transform));
            yield return StartCoroutine(FadeOutTiles(tilesContainer, player));

            yield return new WaitForSeconds(0.2f);

            previousPlayerLocalScaleX = player.transform.localScale.x;
            player.transform.localScale = new Vector2(1f, 1f);

            Ground currentTileGround = Instantiate(groundToInstantiate, transform.position, Quaternion.identity);
            StartCoroutine(FadeInGround(currentTileGround));
            yield return StartCoroutine(FadeInPlayer(player, currentTileGround.playerPostion));

            yield return new WaitForSeconds(0.3f);
        }

        public IEnumerator SetLevelToExplorePhase(Ground groundToDestroy, TilesContainer tilesContainer, Player player)
        {
            StartCoroutine(FadeOutPlayer(player, groundToDestroy.playerPostion));
            yield return StartCoroutine(FadeOutGroundAndDestroy(groundToDestroy));

            yield return new WaitForSeconds(0.2f);

            player.transform.localScale = new Vector2(previousPlayerLocalScaleX, 1f);

            StartCoroutine(FadeInTiles(tilesContainer, player));
            StartCoroutine(FadeOutGroundAndDestroy(groundToDestroy));
            yield return StartCoroutine(FadeInPlayer(player, player.currentTile.transform));

            yield return new WaitForSeconds(0.3f);
        }


        IEnumerator FadeInGround(Ground currentTileGround)
        {
            float yPos = currentTileGround.transform.position.y;
            float alpha = 0f;

            while (alpha <= 1f)
            {
                currentTileGround.transform.position = new Vector2(currentTileGround.transform.position.x, yPos);

                foreach (Transform child in currentTileGround.transform)
                {
                    if (!child.GetComponent<SpriteRenderer>()) continue;

                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                }

                alpha += Time.deltaTime / timeToFade;
                yPos += Time.deltaTime * yOffset / timeToFade;

                yield return null;
            }

            foreach (Transform child in currentTileGround.transform)
            {
                if (!child.GetComponent<SpriteRenderer>()) continue;

                child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        IEnumerator FadeOutGroundAndDestroy(Ground groundToDestory)
        {
            if (groundToDestory)
            {
                float yPos = groundToDestory.transform.position.y;
                float alpha = 1f;

                while (alpha >= 0f)
                {
                    groundToDestory.transform.position = new Vector2(groundToDestory.transform.position.x, yPos);

                    foreach (Transform child in groundToDestory.transform)
                    {
                        if (!child.GetComponent<SpriteRenderer>()) continue;

                        child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                    }

                    yPos -= Time.deltaTime * yOffset / timeToFade;
                    alpha -= Time.deltaTime / timeToFade;

                    yield return null;
                }

                Destroy(groundToDestory.gameObject);
            }
        }

        IEnumerator FadeOutTiles(TilesContainer tilesContainer, Player player)
        {
            float alpha = 1f;

            while (alpha >= tileTrasparentRate)
            {
                for (int i = 0; i < tilesContainer.currentTileList.Count; i++)
                {
                    if (i == player.currentTileIndex) continue;

                    foreach (Transform child in tilesContainer.currentTileList[i].transform)
                    {
                        if (!child.GetComponent<SpriteRenderer>()) continue;

                        child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                    }
                }

                alpha -= Time.deltaTime / timeToFade * (1f - tileTrasparentRate);

                yield return null;
            }
        }

        IEnumerator FadeInTiles(TilesContainer tilesContainer, Player player)
        {
            float alpha = 0.3f;

            while (alpha <= 1f)
            {
                for (int i = 0; i < tilesContainer.currentTileList.Count; i++)
                {
                    if (i == player.currentTileIndex) continue;

                    foreach (Transform child in tilesContainer.currentTileList[i].transform)
                    {
                        if (!child.GetComponent<SpriteRenderer>()) continue;

                        child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                    }
                }

                alpha += Time.deltaTime / timeToFade * 0.7f;

                yield return null;
            }
        }

        IEnumerator FadeInPlayer(Player player, Transform posToFadeIn)
        {
            float alpha = 0f;

            while (alpha <= 1f)
            {
                foreach (Transform child in player.transform)
                {
                    if (!child.GetComponent<SpriteRenderer>()) continue;

                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                }
                player.transform.position = new Vector3(posToFadeIn.position.x, posToFadeIn.position.y, posToFadeIn.position.z);

                alpha += Time.deltaTime / timeToFade;
                yield return null;
            }

            foreach (Transform child in player.transform)
            {
                if (!child.GetComponent<SpriteRenderer>()) continue;

                child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            }
        }

        IEnumerator FadeOutPlayer(Player player, Transform posToFadeOut)
        {
            float alpha = 1f;

            while (alpha >= 0f)
            {
                foreach (Transform child in player.transform)
                {
                    if (!child.GetComponent<SpriteRenderer>()) continue;

                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                }
                player.transform.position = new Vector3(posToFadeOut.position.x, posToFadeOut.position.y, posToFadeOut.position.z);

                alpha -= Time.deltaTime / timeToFade;
                yield return null;
            }

            foreach (Transform child in player.transform)
            {
                if (!child.GetComponent<SpriteRenderer>()) continue;

                child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            }
        }
    }
}
