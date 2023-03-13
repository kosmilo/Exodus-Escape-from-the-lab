using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip buttonSound; // Music by <a href="https://pixabay.com/users/danydory-9903/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=141314">danydory</a> from <a href="https://pixabay.com//?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=141314">Pixabay</a>

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
