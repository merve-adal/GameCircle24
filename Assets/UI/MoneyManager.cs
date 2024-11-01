using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Para miktarýný gösterecek Text referansý
    public TextMeshProUGUI completePanelText; // CompletePanel'deki Text referansý

    private int totalMoney = 0; // Mevcut para miktarý
    private int addMoneyAmount = 3;

    private MenuManager menuManager;

    private void Awake()
    {
        menuManager = GameObject.FindGameObjectWithTag("UILevelMenu").GetComponent<MenuManager>();
    }
    void Start()
    {
        UpdateUI(); // Baþlangýçta UI'yi güncelle
    }

    public void IncreaseMoney()
    {
        totalMoney += addMoneyAmount;
    }

    private void UpdateUI()
    {
        moneyText.text = totalMoney.ToString(); // Text'i güncelle
        completePanelText.text = totalMoney.ToString(); // CompletePanel'deki metni güncelle
    }
}
