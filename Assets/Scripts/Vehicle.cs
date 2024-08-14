using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Vehicle : MonoBehaviour
{
    GameManager gameManager;

    private Sign sign;

    private Way way;

    List<Road> roads = new List<Road>();
    private int activeRoadIndex=0;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotaionSpeed = 2f;

    Router roadManager;

    private bool isMoving = false;
    private bool isMovingInReverse = false;
    
    private float elapsedTimeOnRoad=0;

    private Vector3 firstPositionOnRoad;
    private Vector3 lastPositionOnRoad;
    private void Awake()
    {
        GameObject scriptholder = GameObject.FindGameObjectWithTag("ScriptHolder");
        gameManager = scriptholder.GetComponent<GameManager>();
        roadManager = scriptholder.GetComponent<Router>();

        sign=GetComponentInChildren<SignObject>().Sign;
    }
    private void Start()
    {
        assignFirstWay();
        assignRoads();
    }
    private void Update()
    {
        if (isMoving)
        {
            move();
        }
    }

    private void assignFirstWay()
    {
        float rotationY = transform.rotation.eulerAngles.y;
        if (rotationY < 0)
        {
            rotationY += 360;
        }

        if ( 260<rotationY && rotationY < 280)  //270
        {
            way = Way.Left;
        }
        if (170 < rotationY && rotationY < 190) //180
        {
            way = Way.Down;
        }
        if (80 < rotationY && rotationY < 100) //90
        {
            way = Way.Right;
        }
        else  // 0
        {
            way = Way.Up;
        }

    }

    private void assignRoads()
    {
        if (sign == Sign.Forward)
        {
            roads.Add(roadManager.FindFinishLine(transform.position, way));
        }
        if (sign == Sign.Left)
        {
            roads.Add(roadManager.FindJunction(transform.position, way, Direction.Left));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnLeft();
            roads.Add(roadManager.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
        else if (sign == Sign.Right)
        {
            roads.Add(roadManager.FindJunction(transform.position, way, Direction.Right));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            roads.Add(roadManager.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
        else if (sign == Sign.LeftU)
        {
            roads.Add(roadManager.FindJunction(transform.position, way, Direction.Left));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnLeft();
            roads.Add(roadManager.FindJunction(roads[roads.Count - 1].LastPosition, way, Direction.Left));
            roads.Insert(2, new Road(roads[1].LastPosition, roads[2].FirstPosition));
            changeWayByTurnLeft();
            roads.Add(roadManager.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
        else if (sign == Sign.RightU)
        {
            roads.Add(roadManager.FindJunction(transform.position, way, Direction.Right));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnRight();
            roads.Add(roadManager.FindJunction(roads[roads.Count - 1].LastPosition, way, Direction.Right));
            roads.Insert(2, new Road(roads[1].LastPosition, roads[2].FirstPosition));
            changeWayByTurnRight();
            roads.Add(roadManager.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
    }

    private void changeWayByTurnLeft()
    {
        if (way == Way.Left) {
            way = Way.Down;
        }
        else if (way == Way.Down)
        {
            way = Way.Right;
        }
        else if (way == Way.Right)
        {
            way = Way.Up;
        }
        else if (way == Way.Up)
        {
            way = Way.Left;
        }
    }
    private void changeWayByTurnRight()
    {
        if (way == Way.Left)
        {
            way = Way.Up;
        }
        else if (way == Way.Down)
        {
            way = Way.Left;
        }
        else if (way == Way.Right)
        {
            way = Way.Down;
        }
        else if (way == Way.Up)
        {
            way = Way.Right;
        }
    }

    private void move()
    {
        elapsedTimeOnRoad += Time.deltaTime;

        if (roads[activeRoadIndex].IsJunction && !isMovingInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad * rotaionSpeed;

            transform.position = Vector3.Slerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, fractionOfJourney);
            transform.rotation = Quaternion.Lerp(roads[activeRoadIndex].FirstQuaternion, roads[activeRoadIndex].LastQuaternion, fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                transform.position = roads[activeRoadIndex].LastPosition;
                if (activeRoadIndex != roads.Count - 1)
                {
                    elapsedTimeOnRoad = 0;
                    activeRoadIndex++;
                }               
            }
        }
        else if(!roads[activeRoadIndex].IsJunction && !isMovingInReverse)
        {        
            float fractionOfJourney = elapsedTimeOnRoad * moveSpeed / Vector3.Distance(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition);
            
            transform.position = Vector3.Lerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, fractionOfJourney);
            
            if (fractionOfJourney >= 1)
            {
                transform.position = roads[activeRoadIndex].LastPosition;
                if (activeRoadIndex!=roads.Count-1)
                {
                    elapsedTimeOnRoad = 0;
                    activeRoadIndex++;
                }
                else
                {
                    finish();
                }
                   
            }
        }
        else if(!roads[activeRoadIndex].IsJunction && isMovingInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad * moveSpeed / Vector3.Distance(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition);

            transform.position = Vector3.Lerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, 1-fractionOfJourney);
            
            if (fractionOfJourney >=1)
            {
                transform.position = roads[activeRoadIndex].FirstPosition;
                if (activeRoadIndex >0)
                {
                    elapsedTimeOnRoad = 0;
                    activeRoadIndex--;              
                }
                else
                {
                    isMoving = false;
                    isMovingInReverse = false;
                }

            }

        }
        else if (roads[activeRoadIndex].IsJunction && isMovingInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad * rotaionSpeed;

            transform.position = Vector3.Slerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, 1-fractionOfJourney);
            transform.rotation = Quaternion.Lerp(roads[activeRoadIndex].FirstQuaternion, roads[activeRoadIndex].LastQuaternion, 1-fractionOfJourney);

            if (fractionOfJourney >=1)
            {
                transform.position = roads[activeRoadIndex].FirstPosition;
                if (activeRoadIndex >0)
                {
                    elapsedTimeOnRoad = 0;
                    activeRoadIndex--;
                }
            }
        }
    }

    public void StartMove()
    {
        isMoving = true;
        gameManager.IsPlayable = false;
    }
    public void MoveInReverse()
    {
        isMovingInReverse=true;
    }
    private void finish()
    {
        isMoving = false;
        gameManager.DecreaseNumberOfVehicles();
        gameManager.IsPlayable = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            isMovingInReverse = true;
            elapsedTimeOnRoad = Vector3.Distance(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition) / moveSpeed - elapsedTimeOnRoad;
        }
        else if (other.gameObject.CompareTag("Station"))
        {

        }
    }

    

}
