using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settings;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteractor playerInteractor;
    [SerializeField] PauseMenuManager pauseMenuManager;

    void Start() {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver() {
        // Show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Hide ui, pausemenu and settings, stop player movement and interaction
        pauseMenuManager.enabled = false;
        ui.SetActive(false);
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        playerMovement.enabled = false;
        playerInteractor.enabled = false;

        // Show game over screen
        gameOverScreen.SetActive(true);
    }

    public void ToMainMenu() {
        SceneManager.LoadScene(0);
    }
}