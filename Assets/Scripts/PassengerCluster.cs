using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerCluster
{
    List<List<Passenger>> passengersClassified = new();
    public PassengerCluster()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(PassengerColor)).Length; i++)
        {
            List<Passenger> sameColorPassengers = new();
            passengersClassified.Add(sameColorPassengers);
        }
    }

    public int PassengersCountByColor(PassengerColor color)
    {
        return passengersClassified[(int)color].Count;     
    }
    public List<Passenger> PassengersByColor(PassengerColor color)
    {
        return passengersClassified[(int)color];
    }
    public void AddPassenger(Passenger passenger) {
        passengersClassified[(int)passenger.Color].Add(passenger);
    }
    public void RemovePassengersByColor(PassengerColor color)
    {
        passengersClassified[(int)color].Clear();
    }
}
