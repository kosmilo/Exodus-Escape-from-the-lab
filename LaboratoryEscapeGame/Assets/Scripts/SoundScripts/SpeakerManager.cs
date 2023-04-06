using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpeakerManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip emergencyAnnouncement; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            Debug.Log("Announcement");
            PlayAnnouncement();
        }
    }

    public void PlayAnnouncement() {
        audioSource.clip = emergencyAnnouncement;
        audioSource.loop = false;
        audioSource.Play();
    }
}
