using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI messageShadow;

    private void Start() 
    {
        SetMessage("");    
    }

    public void SetMessage(string message)
    {
        messageText.text = message;
        messageShadow.text = message;
    }
}
