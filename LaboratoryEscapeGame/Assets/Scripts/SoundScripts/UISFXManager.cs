using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip buttonSound; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonSound() {
        audioSource.clip = buttonSound;
        audioSource.loop = false;
        audioSource.Play();
    }
}
