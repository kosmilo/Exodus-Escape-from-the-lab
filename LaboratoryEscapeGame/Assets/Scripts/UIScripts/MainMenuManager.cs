using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject sceneFade;

    void Start() {
        settings.SetActive(false);
        sceneFade = GameObject.Find("SceneFade");
        sceneFade.GetComponent<Animator>().Play("SceneFadeToMainMenu");

        // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame() {
        sceneFade.GetComponent<Animator>().Play("SceneFadeToGame");
        FindObjectOfType<SoundManager>().MusicFadeToGame(14f);
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
        yield return null;
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
