using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Para miktarýný gösterecek Text referansý
    public TextMeshProUGUI completePanelText; // CompletePanel'deki Text referansý

    private int currentMoney = 0; // Mevcut para miktarý

    void Start()
    {
        UpdateUI(); // Baþlangýçta UI'yi güncelle
    }

    public void PassengerBoarded(int numberOfPassengers)
    {
        currentMoney += 3 * numberOfPassengers; // Her yolcu için 100 ekle
        UpdateUI(); // UI'yi güncelle
    }

    private void UpdateUI()
    {
        moneyText.text = currentMoney.ToString(); // Text'i güncelle
        completePanelText.text = currentMoney.ToString(); // CompletePanel'deki metni güncelle
    }
}
