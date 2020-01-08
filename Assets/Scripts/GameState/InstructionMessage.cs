using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI messageShadow;

    private void Start() 
    {
        SetMessage("");    
    }

    public void SetMessage(string message)
    {
        if(messageShadow != null)
        {
            messageShadow.text = message;
        }
        messageText.text = message; 
    }
}
