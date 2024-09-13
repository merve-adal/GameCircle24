using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassangersInfo", menuName = "ScriptableObjects/PassangersInfo")]
public class PassangersInfoScriptableObject : ScriptableObject
{
    [SerializeField] private float radius;
    [SerializeField] private float distanceFactor;

    [SerializeField] private GameObject redCharacterPrefab;
    [SerializeField] private GameObject greenCharacterPrefab;
    [SerializeField] private GameObject blueCharacterPrefab;
    [SerializeField] private GameObject yellowCharacterPrefab;

    public float Radius { get => radius; }
    public float DistanceFactor { get => distanceFactor; }

    public GameObject RedCharacterPrefab { get=> redCharacterPrefab;}
    public GameObject GreenCharacterPrefab { get => greenCharacterPrefab; }
    public GameObject BlueCharacterPrefab { get => blueCharacterPrefab; }
    public GameObject YellowCharacterPrefab { get => yellowCharacterPrefab; }

    
}
