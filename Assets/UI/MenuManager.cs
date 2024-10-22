using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider kullanýmý için gerekli

public class MenuManager : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject settingsPanel;
    public GameObject pausePanel;
    public GameObject AnaPanel;
    public GameObject CompletePanel;
    public GameObject LostPanel;
    public GameObject[] buses; // Otobüsleri tutacak dizi
    public Slider levelSlider; // Level slider'ý tutacak deðiþken

    private bool isCompletePanelActive = false;

    private void Start()
    {
        // Slider'ý baþlangýçta sýfýr yap
        levelSlider.value = 0;
    }

    private void Update()
    {
        CheckBusesInactive();
    }

    public void OnHomeButtonClicked()
    {
        HideAllPanels();
        AnaPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        homePanel.SetActive(false);
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void ShowHomePanel()
    {
        HideAllPanels();
        AnaPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        HideAllPanels();
        settingsPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        HideAllPanels();
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        AnaPanel.SetActive(false);
        CompletePanel.SetActive(false);
        AnaPanel.SetActive(true);
    }

    public void StartNextScene()
    {
        SceneController.LoadNextLevel();
    }

    public void RestartCurrentScene()
    {
        SceneController.RestartCurrentLevel();
    }

    private void CheckBusesInactive()
    {
        bool allBusesInactive = true;

        foreach (var bus in buses)
        {
            if (bus.activeInHierarchy)
            {
                allBusesInactive = false;
                break;
            }
        }

        // Tüm otobüsler inaktif olduðunda CompletePanel'i göster
        if (allBusesInactive && !isCompletePanelActive)
        {
            CompletePanel.SetActive(true);
            isCompletePanelActive = true;
        }
        else
        {
            // Tüm otobüslerin aktif olmadýðýný kontrol et ve slider'ý güncelle
            UpdateLevelSlider();
        }
    }

    private void UpdateLevelSlider()
    {
        // Aktif olmayan otobüs sayýsýný bul
        int inactiveBusCount = 0;
        foreach (var bus in buses)
        {
            if (!bus.activeInHierarchy)
            {
                inactiveBusCount++;
            }
        }

        // Slider'ýn deðerini güncelle
        float fillAmount = (float)inactiveBusCount / buses.Length; // Dolum oraný
        levelSlider.value = fillAmount; // Slider'ý güncelle
    }

}