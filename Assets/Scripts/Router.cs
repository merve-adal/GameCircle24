using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Router : MonoBehaviour
{

    [SerializeField] private FinishLinesScriptableObject finishLinesScriptableObject;


    public Road FindJunction(Vector3 vehiclePosition, Way way, Direction direction)
    {
        Vector3 firstPosition = Vector3.zero;
        Vector3 lastPosition = Vector3.zero;
        Quaternion firstQuaternion = Quaternion.identity;
        Quaternion lastQuaternion = Quaternion.identity;
        firstPosition = _enterPositionOfJunction(vehiclePosition, way, direction);    
        firstQuaternion = Quaternion.Euler(0, (int)way * 90, 0);
        
        int newWay = _newWay(way,direction);

        lastPosition = _exitPositionOfJunction(firstPosition,newWay,direction);
        lastQuaternion = Quaternion.Euler(0, newWay * 90, 0);

        return new Road(firstPosition, lastPosition, firstQuaternion, lastQuaternion);
        
    }
        
    public Road FindFinishLine(Vector3 vehiclePosition, Way way)
    {
        Vector3 finishLine;

        if (way == Way.Left)
        {
            finishLine = new Vector3(finishLinesScriptableObject.LeftFinishLine.x, vehiclePosition.y, vehiclePosition.z);
        }
        else if (way == Way.Down)
        {
            finishLine = new Vector3(vehiclePosition.x, vehiclePosition.y, finishLinesScriptableObject.DownFinishLine.z);
        }
        else if (way == Way.Right)
        {
            finishLine = new Vector3(finishLinesScriptableObject.RightFinishLine.x, vehiclePosition.y, vehiclePosition.z);
        }
        else //up
        {
            finishLine = new Vector3(vehiclePosition.x, vehiclePosition.y, finishLinesScriptableObject.UpFinishLine.z);
        }
        return new Road(vehiclePosition, finishLine);
    }

    private Vector3 _enterPositionOfJunction(Vector3 vehiclePosition,Way way, Direction direction)
    {
        Vector3 firstPosition = Vector3.zero;
        Vector3 raycastDirection = Vector3.zero;
        if (way == Way.Up)
        {
            raycastDirection = new Vector3(0, 0, 1);
        }
        else if (way == Way.Right)
        {
            raycastDirection = new Vector3(1, 0, 0);
        }
        else if (way == Way.Down)
        {
            raycastDirection = new Vector3(0, 0, -1);
        }
        else //Left
        {
            raycastDirection = new Vector3(-1, 0, 0);
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(vehiclePosition, raycastDirection, 50f);

        // if vehicle turns closed way of Troad, bypass Troad
        //  way  =  up 0 ,right -90, down -180, left -270
        //  direction = left -90, right 90
        //  -way + troad + direction == (0 or 360) direct pass Troad

        RaycastHit hit = hits.Where(
            h => (h.transform.CompareTag("Crossroad") || 
            (h.transform.CompareTag("TRoad") && ((-(int)way * 90f + Mathf.RoundToInt(h.transform.rotation.eulerAngles.y) + (int)direction * 90f) % 360 != 0)))
            ).ToList().OrderBy(h => Vector3.Distance(vehiclePosition, h.transform.position)).ToArray()[0];


        return hit.point;
    }
    private int _newWay(Way way, Direction direction)
    {
        int newWay = ((int)way + (int)direction) % (System.Enum.GetValues(typeof(Way)).Length);
        if (newWay == -1)
        {
            newWay = System.Enum.GetValues(typeof(Way)).Length - 1;
        }
        return newWay;
    }
    private Vector3 _exitPositionOfJunction(Vector3 firstPosition, int newWay, Direction direction)
    {
        Vector3 lastPosition = Vector3.zero;

        float lastPositionOffsetX = 0;
        float lastPositionOffsetZ = 0;

        if (newWay == 0) //up
        {
            lastPositionOffsetX = -(int)direction * 0.5f;
            lastPositionOffsetZ = 0.5f;
        }
        else if (newWay == 1) //right
        {
            lastPositionOffsetX = 0.5f;
            lastPositionOffsetZ = (int)direction * 0.5f;
        }
        else if (newWay == 2) //down
        {
            lastPositionOffsetX = (int)direction * 0.5f;
            lastPositionOffsetZ = -0.5f;
        }
        else  //left
        {
            lastPositionOffsetX = -0.5f;
            lastPositionOffsetZ = -(int)direction * 0.5f;
        }

        lastPosition = new Vector3(firstPosition.x + lastPositionOffsetX, firstPosition.y, firstPosition.z+ lastPositionOffsetZ);
        return lastPosition;
    }

}
