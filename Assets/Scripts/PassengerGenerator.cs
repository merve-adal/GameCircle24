using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Unity.EditorCoroutines; //bunu kullanabilirsin

public class PassengerGenerator : MonoBehaviour  //randomly order
{
    [SerializeField] private PassangersInfoScriptableObject passangerInfo;

    [SerializeField] private int numberOfRedCharacter = 0;
    [SerializeField] private int numberOfGreenCharacter = 0;
    [SerializeField] private int numberOfBlueCharacter = 0;
    [SerializeField] private int numberOfYellowCharacter = 0;

    public void Generate()
    {
        int numChildren = transform.GetChild(0).childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).GetChild(i).gameObject);
        }

        int total = 0;
        total += numberOfRedCharacter;
        total += numberOfGreenCharacter;
        total += numberOfBlueCharacter;
        total += numberOfYellowCharacter;

        List<GameObject> tempPassengersList = new();

        addPrefabsToList(passangerInfo.RedCharacterPrefab, numberOfRedCharacter,tempPassengersList);
        addPrefabsToList(passangerInfo.GreenCharacterPrefab, numberOfGreenCharacter, tempPassengersList);
        addPrefabsToList(passangerInfo.BlueCharacterPrefab, numberOfBlueCharacter, tempPassengersList);
        addPrefabsToList(passangerInfo.YellowCharacterPrefab, numberOfYellowCharacter, tempPassengersList);

        for (int i = 0; i < total; i++) {

            int num = UnityEngine.Random.Range(0, tempPassengersList.Count);
            float x = transform.GetChild(0).position.x + passangerInfo.DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * passangerInfo.Radius);
            float z = transform.GetChild(0).position.z + passangerInfo.DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * passangerInfo.Radius);
            Instantiate(tempPassengersList[num], new Vector3(x,0,z), Quaternion.Euler(0,0f,0), transform.GetChild(0));
            tempPassengersList.RemoveAt(num);

        }

    }
    private void addPrefabsToList(GameObject prefab, int numberOfPrefabs, List<GameObject> list)
    {
        for (int i = 0; i < numberOfPrefabs; i++) { 
           list.Add(prefab);
        }
    } 
}
