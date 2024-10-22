using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider kullan�m� i�in gerekli

public class MenuManager : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject settingsPanel;
    public GameObject pausePanel;
    public GameObject AnaPanel;
    public GameObject CompletePanel;
    public GameObject LostPanel;
    public GameObject[] buses; // Otob�sleri tutacak dizi
    public Slider levelSlider; // Level slider'� tutacak de�i�ken

    private bool isCompletePanelActive = false;

    private void Start()
    {
        // Slider'� ba�lang��ta s�f�r yap
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

        // T�m otob�sler inaktif oldu�unda CompletePanel'i g�ster
        if (allBusesInactive && !isCompletePanelActive)
        {
            CompletePanel.SetActive(true);
            isCompletePanelActive = true;
        }
        else
        {
            // T�m otob�slerin aktif olmad���n� kontrol et ve slider'� g�ncelle
            UpdateLevelSlider();
        }
    }

    private void UpdateLevelSlider()
    {
        // Aktif olmayan otob�s say�s�n� bul
        int inactiveBusCount = 0;
        foreach (var bus in buses)
        {
            if (!bus.activeInHierarchy)
            {
                inactiveBusCount++;
            }
        }

        // Slider'�n de�erini g�ncelle
        float fillAmount = (float)inactiveBusCount / buses.Length; // Dolum oran�
        levelSlider.value = fillAmount; // Slider'� g�ncelle
    }

}