using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private bool isBusAtStation = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BUS")) // Otob�s etiketi kontrol edilir
        {
            isBusAtStation = true; // Otob�s dura��n i�ine girdi
            Debug.Log("Bus arrived at the station.");
            NotifyPassengers(); // Yolcular� bilgilendir
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BUS")) // Otob�s etiketi kontrol edilir
        {
            isBusAtStation = false; // Otob�s dura��n d���na ��kt�
            Debug.Log("Bus left the station.");
            NotifyPassengers(); // Yolcular� bilgilendir
        }
    }

    public void NotifyPassengers() // Bu metodu public yap�yoruz
    {
        Color busColor = FindObjectOfType<BUS>().GetComponent<Renderer>().material.color;
        Debug.Log("NotifyPassengers called - Bus Color: " + busColor);
        Passenger[] passengers = GetComponentsInChildren<Passenger>();

        foreach (Passenger passenger in passengers)
        {
            Debug.Log("Notifying passenger: " + passenger.name);
            passenger.CheckBusColor(busColor, isBusAtStation); // Otob�s rengini ve etkile�im durumunu yolculara ilet
        }
    }
}
