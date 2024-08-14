using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Renderer passengerRenderer;
    private Animator[] animators;
    private bool isRunning = false;
    private Transform busTransform;

    void Start()
    {
        // Alt öðelerden Renderer bileþenini bul
        passengerRenderer = GetComponentInChildren<Renderer>();

        if (passengerRenderer == null)
        {
            Debug.LogError("Renderer bileþeni bulunamadý: " + gameObject.name);
            return; // Renderer yoksa devam etme
        }

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

    public void CheckBusColor(Color busColor, bool isAtStation)
    {
        Debug.Log("Bus rengini kontrol et - Yolcu Rengi: " + passengerRenderer.material.color + ", Otobüs Rengi: " + busColor + ", Durakta mý: " + isAtStation);
        isRunning = isAtStation && passengerRenderer.material.color == busColor;

        foreach (var animator in animators)
        {
            animator.SetBool("isRunning", isRunning);
        }

        if (isRunning)
        {
            busTransform = FindObjectOfType<BUS>().transform;
        }
    }

    private void MoveTowardsBus()
    {
        if (busTransform == null) return;

        // Karakteri otobüse doðru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, busTransform.position, Time.deltaTime * 5f);

        // Karakter otobüs pozisyonuna ulaþtýðýnda dur
        if (Vector3.Distance(transform.position, busTransform.position) < 0.1f)
        {
            isRunning = false;
            foreach (var animator in animators)
            {
                animator.SetBool("isRunning", false);
            }

            // Karakteri otobüsün alt öðesi yaparak onunla birlikte hareket etmesini saðla
            transform.SetParent(busTransform);
        }
    }
}
