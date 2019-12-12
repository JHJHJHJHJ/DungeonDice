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

        [HideInInspector]
        public bool isAnimating = false;

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
    }
}

