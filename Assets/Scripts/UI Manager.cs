using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject PopupButton;

    public GameObject PausePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUpgrades()
    {
        UIPanel.SetActive(true);
        PopupButton.SetActive(false);
    }

    public void CloseUpgrades()
    {
        UIPanel.SetActive(false);
        PopupButton.SetActive(true);
    }

    public void OpenPause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ClosePause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
