using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DungeonDice.Tiles;

namespace DungeonDice.UI
{
    public class EventTextBox : MonoBehaviour
    {
        public Image textBoxBackground;
        public TextMeshProUGUI descriptionText;
        [SerializeField] float timeToType = 0.01f;

        public Queue<string> descriptionQueue;

        [HideInInspector]
        public bool isAnimating = false;
        public bool isTyping = false;

        public void Open()
        {   
            isAnimating = true;
            descriptionText.text = "";
            GetComponent<Animator>().SetTrigger("Open");
        }

        public void HasOpened() // 애니메이션에서 실행됨
        {
            isAnimating = false;
        }

        public void Close()
        {
            isAnimating = true;
            descriptionText.text = ""; 
            GetComponent<Animator>().SetTrigger("Close");
        }

        public void HasClosed() // 애니메이션에서 실행됨
        {
            isAnimating = false;
            gameObject.SetActive(false);
        }

        public IEnumerator TypeDescription(string description)
        {
            isTyping = true;

            descriptionText.text = "";

            foreach (char letter in description.ToCharArray())
            {
                descriptionText.text += letter;
                yield return new WaitForSeconds(timeToType);
            }

            isTyping = false;

            yield return new WaitForSeconds(timeToType);
        }

        public void EnqueueDescriptions(string[] descriptions)
        {
            descriptionQueue = new Queue<string>();
            foreach (string description in descriptions)
            {
                descriptionQueue.Enqueue(description);
            }
        }
    }
}

