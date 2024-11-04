using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider kullanýmý için gerekli

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject AnaPanel;
    [SerializeField] private GameObject CompletePanel;
    [SerializeField] private GameObject LostPanel;
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private Slider levelSlider; // Level slider'ý tutacak deðiþken
    [SerializeField] private Slider livesSlider;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text completePanelText;
    [SerializeField] private TMP_Text levelUpText;


    private void Awake()
    {
        levelName.text = SceneController.CurrentLevelNumber().ToString();
        moneyText.text = "$0";
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
        SoundController.PlayClickSound();
    }

    public void ShowSettingsPanel()
    {
        HideAllPanels();
        settingsPanel.SetActive(true);
        SoundController.PlayClickSound();
    }

    public void ShowPausePanel()
    {
        HideAllPanels();
        pausePanel.SetActive(true);
        SoundController.PlayClickSound();
    }
    public void ShowCompletePanel()
    {
        CompletePanel.SetActive(true);
        completePanelText.text = moneyText.text;
        levelUpText.text=SceneController.CurrentLevelNumber().ToString();
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
        SoundController.PlayClickSound();
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
        livesSlider.value = fillAmount;
        livesText.text = lives.ToString()+"/"+ totalLives.ToString();
    }
    public void UpdateMoney(int money)
    {
        moneyText.text = "$"+money.ToString(); // Text'i güncelle
    }

}