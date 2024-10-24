using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Para miktar�n� g�sterecek Text referans�
    public TextMeshProUGUI completePanelText; // CompletePanel'deki Text referans�

    private int totalMoney = 0; // Mevcut para miktar�
    private int addMoneyAmount = 3;

    void Start()
    {
        UpdateUI(); // Ba�lang��ta UI'yi g�ncelle
    }

    public void IncreaseMoney()
    {
        totalMoney += addMoneyAmount;
    }
    //public void PassengerBoarded(int numberOfPassengers)
    //{
    //    currentMoney += 3 * numberOfPassengers; // Her yolcu i�in 100 ekle
    //    UpdateUI(); // UI'yi g�ncelle
    //}

    private void UpdateUI()
    {
        moneyText.text = totalMoney.ToString(); // Text'i g�ncelle
        completePanelText.text = totalMoney.ToString(); // CompletePanel'deki metni g�ncelle
    }
}
