using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // Para miktar�n� g�sterecek Text referans�
    public TextMeshProUGUI completePanelText; // CompletePanel'deki Text referans�

    private int totalMoney = 0; // Mevcut para miktar�
    private int addMoneyAmount = 3;

    private MenuManager menuManager;

    private void Awake()
    {
        menuManager = GameObject.FindGameObjectWithTag("UILevelMenu").GetComponent<MenuManager>();
    }
    void Start()
    {
        UpdateUI(); // Ba�lang��ta UI'yi g�ncelle
    }

    public void IncreaseMoney()
    {
        totalMoney += addMoneyAmount;
    }

    private void UpdateUI()
    {
        moneyText.text = totalMoney.ToString(); // Text'i g�ncelle
        completePanelText.text = totalMoney.ToString(); // CompletePanel'deki metni g�ncelle
    }
}
