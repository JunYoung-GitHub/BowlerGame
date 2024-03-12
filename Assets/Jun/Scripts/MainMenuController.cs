using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject ballSelectCanvas;
    public Button quitButton;
    public GameObject CreditsPanel;

    private void Start() {
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    public void OnStartButtonClicked() {
        SceneManager.LoadScene("GameScene");
    }

    public void OnCreditButtonClicked() {
      CreditsPanel.SetActive(true);
    }

    public void OnCreditQuitButtonClicked() {
        CreditsPanel.SetActive(false);
    }

    public void OnBallSelectButtonClicked() {
        ballSelectCanvas.SetActive(true);
    }

    public void OnBallSelectQuitButtonClicked() {
        ballSelectCanvas.SetActive(false);
    }

    private void OnQuitButtonClicked() {
        Application.Quit();
    }
}
