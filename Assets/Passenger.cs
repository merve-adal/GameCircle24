using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Renderer passengerRenderer;
    private Animator[] animators; // Animator bileþenlerinin dizisi

    void Start()
    {
        passengerRenderer = GetComponent<Renderer>();

        // 'Passenger' baþlýðý altýndaki tüm çocuk objelerdeki Animator bileþenlerini al
        animators = GetComponentsInChildren<Animator>();

        // Baþlangýçta tüm animasyonlarý kapat
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
            animator.SetBool("isRunning", shouldRun); // Koþma animasyonunu kontrol et
        }

        if (shouldRun)
        {
            MoveTowardsBus(); // Otobüse doðru hareket et
        }
    }

    private void MoveTowardsBus()
    {
        Vector3 busPosition = FindObjectOfType<BUS>().transform.position;
        transform.position = Vector3.MoveTowards(transform.position, busPosition, Time.deltaTime * 5f);
    }
}
