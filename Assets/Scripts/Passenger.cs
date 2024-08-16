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
        // 'Passenger' ba�l��� alt�ndaki t�m �ocuk objelerdeki Animator bile�enlerini al
        animators = GetComponentsInChildren<Animator>();

        // Ba�lang��ta t�m animasyonlar� kapat
        foreach (var animator in animators)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Update()
    {
        // Ko�ma durumu varsa karakteri otob�se do�ru hareket ettir
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

        // Karakteri otob�se do�ru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, vehicleTransform.position, Time.deltaTime * 5f);

        // Karakter otob�s pozisyonuna ula�t���nda dur
        if (Vector3.Distance(transform.position, vehicleTransform.position) < 0.1f)
        {
            isRunning = false;
            foreach (var animator in animators)
            {
                animator.SetBool("isRunning", false);
            }

            // Karakteri otob�s�n alt ��esi yaparak onunla birlikte hareket etmesini sa�la

            transform.SetParent(vehicleTransform);
            vehicleTransform.GetComponent<Vehicle>().PickUpPassenger();
        }
    }
}
