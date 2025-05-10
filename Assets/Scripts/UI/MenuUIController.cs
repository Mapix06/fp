using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public GameObject bgUI;
    public GameObject exitPanel;
    public GameObject optionsPanel;
    public GameObject controlsPanel;
    public GameObject creditsPanel;
    public GameObject soundPanel;

    void Start()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        if (bgUI != null) bgUI.SetActive(true);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (controlsPanel != null) controlsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (soundPanel != null) soundPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        if (bgUI != null) bgUI.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void OpenControls()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (controlsPanel != null) controlsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void OpenSound()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (soundPanel != null) soundPanel.SetActive(true);
    }

    public void OpenExit()
    {
        if (bgUI != null) bgUI.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(true);
    }

    public void BackFromOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (bgUI != null) bgUI.SetActive(true);
    }

    public void BackFromControls()
    {
        if (controlsPanel != null) controlsPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void BackFromCredits()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void BackFromSound()
    {
        if (soundPanel != null) soundPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void CancelExit()
    {
        if (exitPanel != null) exitPanel.SetActive(false);
        if (bgUI != null) bgUI.SetActive(true);
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Introduccion");
    }
}
