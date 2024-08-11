using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Renderer passengerRenderer;
    private Animator[] animators; // Animator bile�enlerinin dizisi

    void Start()
    {
        passengerRenderer = GetComponent<Renderer>();

        // 'Passenger' ba�l��� alt�ndaki t�m �ocuk objelerdeki Animator bile�enlerini al
        animators = GetComponentsInChildren<Animator>();

        // Ba�lang��ta t�m animasyonlar� kapat
        foreach (var animator in animators)
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void CheckBusColor(Color busColor, bool isAtStation)
    {
        bool shouldRun = isAtStation && passengerRenderer.material.color == busColor;

        foreach (var animator in animators)
        {
            animator.SetBool("isRunning", shouldRun); // Ko�ma animasyonunu kontrol et
        }

        if (shouldRun)
        {
            MoveTowardsBus(); // Otob�se do�ru hareket et
        }
    }

    private void MoveTowardsBus()
    {
        Vector3 busPosition = FindObjectOfType<BUS>().transform.position;
        transform.position = Vector3.MoveTowards(transform.position, busPosition, Time.deltaTime * 5f);
    }
}
