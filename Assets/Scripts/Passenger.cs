using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Renderer passengerRenderer;
    private Animator[] animators;
    private bool isRunning = false;
    private Transform vehicleTransform;

    [SerializeField] private PassengerColor color;
    public PassengerColor Color { get => color; }
    void Start()
    {
        // 'Passenger' baþlýðý altýndaki tüm çocuk objelerdeki Animator bileþenlerini al
        animators = GetComponentsInChildren<Animator>();

        // Baþlangýçta tüm animasyonlarý kapat
        foreach (var animator in animators)
        {
            animator.SetBool("isRunning", false);
        }
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
        foreach (var animator in animators)
        {
            animator.SetBool("isRunning", isRunning);
        }
    }

    private void MoveTowardsBus()
    {
        if (vehicleTransform == null) return;

        // Karakteri otobüse doðru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, vehicleTransform.position, Time.deltaTime * 5f);

        // Karakter otobüs pozisyonuna ulaþtýðýnda dur
        if (Vector3.Distance(transform.position, vehicleTransform.position) < 0.1f)
        {
            isRunning = false;
            foreach (var animator in animators)
            {
                animator.SetBool("isRunning", false);
            }

            // Karakteri otobüsün alt öðesi yaparak onunla birlikte hareket etmesini saðla

            transform.SetParent(vehicleTransform);
            vehicleTransform.GetComponent<Vehicle>().PickUpPassenger();
        }
    }
}
