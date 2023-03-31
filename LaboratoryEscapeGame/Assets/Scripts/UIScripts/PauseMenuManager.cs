using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject player;
    PlayerMovement playerMovement;
    PlayerInteractor playerInteractor;
    PlayerSoundEffects playerSoundEffects;

    bool isGamePaused;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInteractor = player.GetComponent<PlayerInteractor>();
        playerSoundEffects = player.GetComponent<PlayerSoundEffects>();

        isGamePaused = false;
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        playerMovement.enabled = true;
        playerInteractor.enabled = true;

        // Lock and hide the cursor while the game is playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Make sure the timescale is 1
        Time.timeScale = 1;
    }

    void Update()
    {
        // Pause Game when esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) { PauseOrResumeGame(); }
    }

    public void PauseOrResumeGame() {
        isGamePaused = !isGamePaused;
        hud.SetActive(!isGamePaused);
        pauseMenu.SetActive(isGamePaused);
        settings.SetActive(false);

        // Change timescale to stop/continue animations and movement
        Time.timeScale = isGamePaused ? 0 : 1;

        // Disable/enable player movement and interaction
        playerMovement.enabled = !isGamePaused;
        playerInteractor.enabled = !isGamePaused;

        // Lock/Unlock cursor 
        if (!isGamePaused) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSoundEffects.StopMovementSounds();
            playerSoundEffects.StopOutOfBreathSound();
        }
    }

    public void OpenSettings() {
        settings.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseSettings() {
        settings.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
