using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    GameManager gameManager;
    //MoneyManager moneyManager;
    Ticket ticket;

    private Sign sign;

    private Way way;

    private int ticketPrice = 3;

    [SerializeField] private PassengerColor passengerColor;
    public PassengerColor Color { get => passengerColor; }

    List<Road> roads = new List<Road>();
    private int activeRoadIndex = 0;

    [SerializeField] private float moveSpeed = 5f;

    private readonly float QuarterCircleLength = 0.785f;

    Router router;

    private bool isMoving = false;
    private bool isInReverse = false;
    public bool IsInReverse { get => isInReverse; }

    private float elapsedTimeOnRoad = 0;

    private Vector3 firstPositionOnRoad;
    private Vector3 lastPositionOnRoad;

    private int numberOfWaitingPassengers = 0;

    private bool isTicketAvailable = false; //activates ticket on the first passenger getting on a bus at the station

    private bool isOnFinishLevel = false;
    public bool IsOnFinishLevel { get => isOnFinishLevel; }

    private Animator vehicleAnimator;

    //vehicle leaves the screen
    Plane[] cameraFrustum;
    Collider vehicleCollider;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        //moneyManager = GameObject.FindWithTag("ScriptHolder").GetComponent<MoneyManager>();
        router = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Router>();
        sign= GetComponentInChildren<SignObject>().Sign;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        vehicleCollider = GetComponent<Collider>();
        vehicleAnimator = GetComponent<Animator>();
        ticket=GameObject.FindGameObjectWithTag("Ticket").GetComponent<Ticket>();

    }
    private void Start()
    {
        assignFirstWay();
        assignRoads();
    }
    private void FixedUpdate()
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

        if (260 < rotationY && rotationY < 280)  //270
        {
            way = Way.Left;
        }
        else if (170 < rotationY && rotationY < 190) //180
        {
            way = Way.Down;
        }
        else if (80 < rotationY && rotationY < 100) //90
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
            roads.Add(router.FindFinishLine(transform.position, way));
        }
        else if (sign == Sign.Left)
        {
            roads.Add(router.FindJunction(transform.position, way, Direction.Left));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnLeft();
            roads.Add(router.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
        else if (sign == Sign.Right)
        {
            roads.Add(router.FindJunction(transform.position, way, Direction.Right));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnRight();
            roads.Add(router.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
        else if (sign == Sign.LeftU)
        {
            roads.Add(router.FindJunction(transform.position, way, Direction.Left));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnLeft();
            roads.Add(router.FindJunction(roads[roads.Count - 1].LastPosition, way, Direction.Left));
            roads.Insert(2, new Road(roads[1].LastPosition, roads[2].FirstPosition));
            changeWayByTurnLeft();
            roads.Add(router.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
        else if (sign == Sign.RightU)
        {
            roads.Add(router.FindJunction(transform.position, way, Direction.Right));
            roads.Insert(0, new Road(transform.position, roads[0].FirstPosition));
            changeWayByTurnRight();
            roads.Add(router.FindJunction(roads[roads.Count - 1].LastPosition, way, Direction.Right));
            roads.Insert(2, new Road(roads[1].LastPosition, roads[2].FirstPosition));
            changeWayByTurnRight();
            roads.Add(router.FindFinishLine(roads[roads.Count - 1].LastPosition, way));
        }
    }

    private void changeWayByTurnLeft()
    {
        if (way == Way.Left)
        {
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

        if (roads[activeRoadIndex].IsJunction && !isInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad *moveSpeed / QuarterCircleLength;

            transform.position = Vector3.Slerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, fractionOfJourney);
            transform.rotation = Quaternion.Lerp(roads[activeRoadIndex].FirstQuaternion, roads[activeRoadIndex].LastQuaternion, fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                elapsedTimeOnRoad = 0;
                transform.position = roads[activeRoadIndex].LastPosition;

                if (activeRoadIndex != roads.Count - 1)
                {                   
                    activeRoadIndex++;
                }
            }
        }
        else if (!roads[activeRoadIndex].IsJunction && !isInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad * moveSpeed / Vector3.Distance(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition);
            transform.position = Vector3.Lerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, fractionOfJourney);

            //vehicle leaves the screen
            Bounds bounds = vehicleCollider.bounds;
            if (!GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
            {
                fractionOfJourney = 1;
            }

            if (fractionOfJourney >= 1)
            {
                elapsedTimeOnRoad = 0;
                transform.position = roads[activeRoadIndex].LastPosition;

                if (activeRoadIndex != roads.Count - 1)
                {
                    activeRoadIndex++;
                }
                else
                {
                    finish();
                }

            }
        }
        else if (!roads[activeRoadIndex].IsJunction && isInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad * moveSpeed / Vector3.Distance(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition);

            transform.position = Vector3.Lerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, 1 - fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                elapsedTimeOnRoad = 0;
                transform.position = roads[activeRoadIndex].FirstPosition;
           
                if (activeRoadIndex > 0)
                {             
                    activeRoadIndex--;
                }
                else
                {
                    stopMove();
                    isInReverse = false;
                    gameManager.IsPlayable = true;
                }

            }

        }
        else if (roads[activeRoadIndex].IsJunction && isInReverse)
        {
            float fractionOfJourney = elapsedTimeOnRoad *moveSpeed / QuarterCircleLength;

            transform.position = Vector3.Slerp(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition, 1 - fractionOfJourney);
            transform.rotation = Quaternion.Lerp(roads[activeRoadIndex].FirstQuaternion, roads[activeRoadIndex].LastQuaternion, 1 - fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                elapsedTimeOnRoad = 0;
                transform.position = roads[activeRoadIndex].FirstPosition;

                if (activeRoadIndex > 0)
                {            
                    activeRoadIndex--;
                }
            }
        }
    }

    public void StartMove()
    {
        isMoving = true;
        gameManager.IsPlayable = false;
        vehicleAnimator.SetBool("isMoving", true);
    }
    private void stopMove()
    {
        isMoving = false;
        vehicleAnimator.SetBool("isMoving", false);
    }
    private void finish()
    {
        stopMove();
        gameManager.DecreaseNumberOfVehicles();
        gameManager.IsPlayable = true;
        this.gameObject.SetActive(false);
        isOnFinishLevel = true;
    }
    public void StopForPassengers(int _numberOfWaitingPassengers)
    {
        stopMove();
        numberOfWaitingPassengers += _numberOfWaitingPassengers;
        isTicketAvailable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMoving && other.gameObject.CompareTag("Vehicle")) //crash
        {         
            gameManager.DecreaseLives();
            if (!gameManager.IsPlayable && gameManager.Lives<=0)
            {
                stopMove();
            }
            else
            {             
                isInReverse = true;
                elapsedTimeOnRoad = Vector3.Distance(roads[activeRoadIndex].FirstPosition, roads[activeRoadIndex].LastPosition) / moveSpeed - elapsedTimeOnRoad;
                SoundController.PlayCrashSound();
            }          
        }
    }
    public void PickUpPassenger()
    {
        numberOfWaitingPassengers--;
        gameManager.IncreaseMoney(ticketPrice);
        if (isTicketAvailable)
        {
            ticket.Activate(transform.position);
            isTicketAvailable = false;
        }

        if (numberOfWaitingPassengers == 0)
        {
            StartMove();
        }
    }

}
