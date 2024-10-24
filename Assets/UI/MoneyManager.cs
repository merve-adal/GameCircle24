using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Para miktarýný gösterecek Text referansý
    public TextMeshProUGUI completePanelText; // CompletePanel'deki Text referansý

    private int totalMoney = 0; // Mevcut para miktarý
    private int addMoneyAmount = 3;

    void Start()
    {
        UpdateUI(); // Baþlangýçta UI'yi güncelle
    }

    public void IncreaseMoney()
    {
        totalMoney += addMoneyAmount;
    }
    //public void PassengerBoarded(int numberOfPassengers)
    //{
    //    currentMoney += 3 * numberOfPassengers; // Her yolcu için 100 ekle
    //    UpdateUI(); // UI'yi güncelle
    //}

    private void UpdateUI()
    {
        moneyText.text = totalMoney.ToString(); // Text'i güncelle
        completePanelText.text = totalMoney.ToString(); // CompletePanel'deki metni güncelle
    }
}
