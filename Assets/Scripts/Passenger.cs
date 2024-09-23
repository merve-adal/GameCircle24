using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Renderer passengerRenderer;
    private Animator characterAnimator;
    private bool isRunning = false;
    private Transform vehicleTransform;

    private readonly float runningSpeed =1f;

    [SerializeField] private PassengerColor color;
    public PassengerColor Color { get => color; }

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Koþma durumu varsa karakteri otobüse doðru hareket ettir
        if (isRunning)
        {
            MoveTowardsBus();
        }
    }

    public void StartMove(Transform _vehicleTransform)
    {
        vehicleTransform = _vehicleTransform;
        isRunning = true;
        //characterAnimator.SetBool("isRunning", true);
    }

    private void MoveTowardsBus()
    {
        if (vehicleTransform == null) return;

        // Karakteri otobüse doðru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, vehicleTransform.position, Time.deltaTime * runningSpeed);

        // Karakter otobüs pozisyonuna ulaþtýðýnda dur
        if (Vector3.Distance(transform.position, vehicleTransform.position) < 0.1f)
        {
            isRunning = false;

            //characterAnimator.SetBool("isRunning", false);

            // Karakteri otobüsün alt öðesi yaparak onunla birlikte hareket etmesini saðla

            transform.SetParent(vehicleTransform);
            this.gameObject.SetActive(false);
            vehicleTransform.GetComponent<Vehicle>().PickUpPassenger();
        }
    }
}
