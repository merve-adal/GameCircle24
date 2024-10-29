using TMPro;
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
    public TMP_Text levelName;
    public Slider levelSlider; // Level slider'ý tutacak deðiþken
    public Slider livesSlider;
    public TMP_Text livesText;


    private void Awake()
    {
        levelName.text = SceneController.CurrentLevelNumber().ToString();
    }
    private void Start()
    {
        // Slider'ý baþlangýçta sýfýr yap
        levelSlider.value = 0;
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
    public void ShowCompletePanel()
    {
        CompletePanel.SetActive(true);
    }
    public void ShowLostPanel()
    {
        LostPanel.SetActive(true);
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
    public void UpdateLevelSlider(int numberOfVehicles, int totalNumberOfVehicles)
    {
        float fillAmount = (float)(totalNumberOfVehicles - numberOfVehicles) / (float)totalNumberOfVehicles;
        levelSlider.value = fillAmount; // Slider'ý güncelle
    }
    public void UpdateLives(int lives, int totalLives)
    {
        float fillAmount = (float)lives / (float)totalLives;
        livesSlider.value = fillAmount;Debug.Log(fillAmount);
        livesText.text = lives.ToString()+"/"+ totalLives.ToString();
    }

}