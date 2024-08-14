using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;
    private bool isJunction=false;
    private Quaternion firstQuaternion;
    private Quaternion lastQuaternion;

    public Vector3 FirstPosition { get => firstPosition; }
    public Vector3 LastPosition { get => lastPosition; }
    public bool IsJunction { get => isJunction; }
    public Quaternion FirstQuaternion { get => firstQuaternion; }
    public Quaternion LastQuaternion { get => lastQuaternion; }


    public Road(Vector3 firstPosition, Vector3 lastPosition)
    {
        this.firstPosition = firstPosition;
        this.lastPosition = lastPosition;
    }
    public Road(Vector3 firstPosition, Vector3 lastPosition, Quaternion firstQuaternion, Quaternion lastQuaternion)
    {
        this.firstPosition = firstPosition;
        this.lastPosition = lastPosition;
        this.firstQuaternion = firstQuaternion;
        this.lastQuaternion = lastQuaternion;
        isJunction = true;
    }
}