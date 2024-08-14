using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUS : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Station")) // Durak etiketi kontrol edilir
        {
            Station station = other.GetComponent<Station>();
            if (station != null)
            {
                station.NotifyPassengers(); // Yolcularý bilgilendir
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Station")) // Durak etiketi kontrol edilir
        {
            Station station = other.GetComponent<Station>();
            if (station != null)
            {
                station.NotifyPassengers(); // Yolcularý bilgilendir
            }
        }
    }
}
