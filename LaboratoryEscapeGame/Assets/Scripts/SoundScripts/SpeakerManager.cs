using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpeakerManager : MonoBehaviour
{
    AudioSource speakerSource;
    AudioSource alarmSource;
    [SerializeField] AudioClip emergencyAnnouncement;
    [SerializeField] AudioClip alarm;

    private void Awake() {
        // Get references
        speakerSource = GetComponent<AudioSource>();
        alarmSource = transform.GetChild(0).GetComponent<AudioSource>();
    }
    
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // If the lab scene is loaded, play the announcement sound
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            Debug.Log("Announcement");
            PlayAnnouncement();
        }
    }

    // Set the alarm and announcement volumes to 0, play the sounds and start fading them in
    public void PlayAnnouncement() {
        speakerSource.volume = 0;
        speakerSource.clip = emergencyAnnouncement;
        speakerSource.loop = false;
        speakerSource.Play();

        alarmSource.volume = 0;
        alarmSource.clip = alarm;
        alarmSource.loop = false;
        alarmSource.Play();

        StartCoroutine(FadeSoundIn());
    }

    // Fade in the alarm and the announcement sound
    IEnumerator FadeSoundIn()
    {
        while (speakerSource.volume < 1)
        {
            speakerSource.volume += Time.deltaTime * .4f;

            if (alarmSource.volume < 0.5f) {
                alarmSource.volume += Time.deltaTime * .2f;
            }

            yield return 0;
        }
        yield return null;
    }
}
