using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private Transform gesture;
    // Start is called before the first frame update

    private void Awake()
    {
        gameManager=GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
    }
    void Start()
    {
        gameManager.IsPlayable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
