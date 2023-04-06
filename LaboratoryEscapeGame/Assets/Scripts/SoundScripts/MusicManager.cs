using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameAmbient;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayMainMenuMusic();
        }
        else {
            PlayGameAmbient();
        }
    }

    public void PlayMainMenuMusic() {
        audioSource.clip = mainMenuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayGameAmbient()
    {
        audioSource.clip = gameAmbient;
        audioSource.loop = true;
        audioSource.Play();
    }
}
