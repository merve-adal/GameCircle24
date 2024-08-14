using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapController : MonoBehaviour
{

    GameManager gameManager;

    private void Awake()
    {
        gameManager= GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsPlayable && Input.GetMouseButtonDown(0))
        {
            sendRaycast();
        }
    }

    private void sendRaycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.transform.CompareTag("Vehicle"))
            {
                hit.transform.GetComponent<Vehicle>().StartMove();
            }
            
        }

    }
}
