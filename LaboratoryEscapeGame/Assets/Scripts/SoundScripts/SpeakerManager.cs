using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip emergencyAnnouncement; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAnnouncement();
    }

    public void PlayAnnouncement() {
        audioSource.clip = emergencyAnnouncement;
        audioSource.loop = false;
        audioSource.Play();
    }
}
