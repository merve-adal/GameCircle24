using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapController : MonoBehaviour
{

    GameManager gameManager;
    string hitTag = "Vehicle";

    private void Awake()
    {
        gameManager= GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.IsPlayable && Input.GetMouseButtonDown(0))
        {
            sendRay();
        }
    }

    private void sendRay()
    {
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 50f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag(hitTag))
            {
                hit.transform.GetComponent<Vehicle>().StartMove();
            }
        }
    }
}
