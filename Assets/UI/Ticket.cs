using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{

    private Transform movingObject;
    private Vector3 firstLocalPosition = Vector3.zero;
    private Vector3 lastLocalPosition = new Vector3(0, 2.5f, 0f);
        
    private float elapsedTime=0;
    private float totalTime = 0.4f;

    private bool isMoving = false;

    private int numberOfTickets = 0;
    private void Awake()
    {
        movingObject = transform.GetChild(0);        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            move();
        }
    }

    private void move()
    {
        elapsedTime += Time.deltaTime;
        float fractionOfJourney = elapsedTime / totalTime;
        movingObject.localPosition = Vector3.Lerp(firstLocalPosition, lastLocalPosition, fractionOfJourney);

        if (fractionOfJourney >= 1) 
        {
            isMoving = false;
            movingObject.gameObject.SetActive(false);
        }

    }

    public void Activate(Vector3 vehiclePosition)
    {
        this.transform.position = new Vector3 (vehiclePosition.x, 1, vehiclePosition.z);
        movingObject.localPosition = firstLocalPosition;
        elapsedTime = 0;
        movingObject.gameObject.SetActive(true);
        isMoving = true;
    }
}
