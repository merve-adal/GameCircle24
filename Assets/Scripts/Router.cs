using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Router : MonoBehaviour
{

    private List<Vector3> junctions = new List<Vector3>();

    [SerializeField] private FinishLinesScriptableObject finishLinesScriptableObject;

    // Start is called before the first frame update
    private void Awake()
    {
        assignJunctions();
    }

    public Road FindJunction(Vector3 vehiclePosition, Way way, Direction direction)
    {
        Vector3 firstPosition = Vector3.zero;
        Vector3 lastPosition = Vector3.zero;
        Quaternion firstQuaternion = Quaternion.identity;
        Quaternion lastQuaternion = Quaternion.identity;

        Vector3 junctionPosition =  Vector3.zero;

        if (way == Way.Up)
        {
            junctionPosition = junctions.Where(v => (Mathf.Abs(v.x - vehiclePosition.x) < 0.1f && (v.z > vehiclePosition.z))).ToList().OrderBy(v => v.z).ToList()[0];
        }
        else if (way == Way.Right)
        {
            junctionPosition = junctions.Where(v => (Mathf.Abs(v.z - vehiclePosition.z) < 0.1f && (v.x > vehiclePosition.x))).ToList().OrderBy(v => v.x).ToList()[0];
        }
        else if (way == Way.Down)
        {
            junctionPosition = junctions.Where(v => (Mathf.Abs(v.x - vehiclePosition.x) < 0.1f && (v.z < vehiclePosition.z))).ToList().OrderByDescending(v => v.z).ToList()[0];
        }
        else if (way == Way.Left)
        {
            junctionPosition = junctions.Where(v => (Mathf.Abs(v.z - vehiclePosition.z) < 0.1f && (v.x < vehiclePosition.x))).ToList().OrderByDescending(v => v.x).ToList()[0];
        }                 

        firstPosition = junctionEnterPosition(junctionPosition,way);
        firstQuaternion = Quaternion.Euler(0, (int)way * 90, 0);

        int newWay = ((int)way + (int)direction)%(System.Enum.GetValues(typeof(Way)).Length); //bu alisiyormu kontrol ets
        if(newWay == -1)
        {
            newWay = System.Enum.GetValues(typeof(Way)).Length-1;
        }

        lastPosition = junctionExitPosition(junctionPosition, (Way)newWay);
        lastQuaternion = Quaternion.Euler(0, newWay * 90, 0);

        return new Road(firstPosition,lastPosition,firstQuaternion, lastQuaternion);
    }
    
    public Road FindFinishLine(Vector3 vehiclePosition, Way way)
    {
        Vector3 finishLine;

        if (way == Way.Left)
        {
            finishLine = new Vector3(finishLinesScriptableObject.LeftFinishLine.x,vehiclePosition.y,vehiclePosition.z);
        }
        else if (way == Way.Down)
        {
            finishLine = new Vector3(vehiclePosition.x,vehiclePosition.y, finishLinesScriptableObject.DownFinishLine.z);
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

    private Vector3 junctionEnterPosition(Vector3 junctionPosition,Way way)
    {
        return new Vector3(junctionPosition.x + (((int)way - 2) % 2) * 0.5f, junctionPosition.y, junctionPosition.z + (((int)way - 1)) % 2 * 0.5f);
    }
    private Vector3 junctionExitPosition(Vector3 junctionPosition, Way way)
    {
        return new Vector3(junctionPosition.x + (-((int)way - 2) % 2) * 0.5f, junctionPosition.y, junctionPosition.z + (-((int)way - 1)) % 2 * 0.5f);
    }

    private void assignJunctions()
    {
        GameObject[] junctionGameObjects = GameObject.FindGameObjectsWithTag("Junction");
        for (int i = 0; i < junctionGameObjects.Length; i++)
        {
            junctions.Add(junctionGameObjects[i].transform.position);
        }
    }
}
