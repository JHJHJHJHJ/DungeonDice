using UnityEngine;

public class Ground : MonoBehaviour 
{
    [SerializeField] TileType tileType;

    public Transform playerPostion;    

    public GameObject groundObject;
    public GameObject additionalObject;

    public GameObject[] shopItemObjects;
}