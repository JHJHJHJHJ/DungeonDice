using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DungeonInfo : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI floorText;

    StateHolder stateHolder;

    private void Awake() 
    {
        stateHolder = FindObjectOfType<StateHolder>();
    }

    private void Update() 
    {
        floorText.text = stateHolder.GetCurrentFloor() + "F";    
    }
}