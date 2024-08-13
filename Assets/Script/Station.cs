using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private bool isBusAtStation = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BUS")) // Otobüs etiketi kontrol edilir
        {
            isBusAtStation = true; // Otobüs duraðýn içine girdi
            Debug.Log("Bus arrived at the station.");
            NotifyPassengers(); // Yolcularý bilgilendir
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BUS")) // Otobüs etiketi kontrol edilir
        {
            isBusAtStation = false; // Otobüs duraðýn dýþýna çýktý
            Debug.Log("Bus left the station.");
            NotifyPassengers(); // Yolcularý bilgilendir
        }
    }

    public void NotifyPassengers() // Bu metodu public yapýyoruz
    {
        Color busColor = FindObjectOfType<BUS>().GetComponent<Renderer>().material.color;
        Debug.Log("NotifyPassengers called - Bus Color: " + busColor);
        Passenger[] passengers = GetComponentsInChildren<Passenger>();

        foreach (Passenger passenger in passengers)
        {
            Debug.Log("Notifying passenger: " + passenger.name);
            passenger.CheckBusColor(busColor, isBusAtStation); // Otobüs rengini ve etkileþim durumunu yolculara ilet
        }
    }
}
