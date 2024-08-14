using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPlayable = true;
    public bool IsPlayable { get => isPlayable; set => isPlayable = value; }

    private int numberOfVehicles = 0;

    private void Awake()
    {
        numberOfVehicles = GameObject.FindGameObjectsWithTag("Vehicle").Length;
    }
    public void DecreaseNumberOfVehicles()
    {
        numberOfVehicles--;
        if (numberOfVehicles == 0)
        {
            Debug.Log("level completed");
        }
    }
}
