using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settings;

    void Start() {
        settings.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings() {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings() {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
