using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{

    PassengerCluster passengerCluster = new PassengerCluster();

    private void Awake()
    {
        Passenger[] passengers = GetComponentsInChildren<Passenger>();
        foreach (Passenger passenger in passengers)
        {
            passengerCluster.AddPassenger(passenger);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle")) // Otobüs etiketi kontrol edilir
        {
            Debug.Log("Bus arrived at the station.");

            Vehicle vehicle = other.transform.GetComponent<Vehicle>();
            int numberOfPassengers = passengerCluster.PassengersCountByColor(vehicle.Color);
            if (numberOfPassengers > 0)
            {
                vehicle.StopForPassengers(numberOfPassengers);
                //Notify passengers
                List<Passenger> passengers = passengerCluster.PassengersByColor(vehicle.Color);
                foreach (Passenger passenger in passengers)
                {
                    Debug.Log("Notifying passenger: " + passenger.name);
                    passenger.StartMove(other.transform); 
                }
                passengerCluster.RemovePassengersByColor(vehicle.Color);
            }           
        }
    }
}
