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
        // Alt ��elerden Renderer bile�enini bul
        passengerRenderer = GetComponentInChildren<Renderer>();

        if (passengerRenderer == null)
        {
            Debug.LogError("Renderer bile�eni bulunamad�: " + gameObject.name);
            return; // Renderer yoksa devam etme
        }

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

    public void CheckBusColor(Color busColor, bool isAtStation)
    {
        Debug.Log("Bus rengini kontrol et - Yolcu Rengi: " + passengerRenderer.material.color + ", Otob�s Rengi: " + busColor + ", Durakta m�: " + isAtStation);
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

        // Karakteri otob�se do�ru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, busTransform.position, Time.deltaTime * 5f);

        // Karakter otob�s pozisyonuna ula�t���nda dur
        if (Vector3.Distance(transform.position, busTransform.position) < 0.1f)
        {
            isRunning = false;
            foreach (var animator in animators)
            {
                animator.SetBool("isRunning", false);
            }

            // Karakteri otob�s�n alt ��esi yaparak onunla birlikte hareket etmesini sa�la
            transform.SetParent(busTransform);
        }
    }
}
