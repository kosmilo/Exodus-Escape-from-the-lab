using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Collection of methods that are called from other scripts to play sounds

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource movementAudioSource;
    [SerializeField] AudioSource breathingAudioSource;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip runSound;
    [SerializeField] AudioClip outOfBreathSound;

    // If walk sound isn't playing, play walk sound
    public void PlayWalkingSound() {
        if (!(movementAudioSource.clip == walkSound && movementAudioSource.isPlaying)) { 
            movementAudioSource.clip = walkSound;
            movementAudioSource.loop = true;
            movementAudioSource.Play();
         }
    }

    // If run sound isn't playing, play run sound
    public void PlayRunningSound() {
        if (!(movementAudioSource.clip == runSound && movementAudioSource.isPlaying)) { 
            movementAudioSource.clip = runSound;
            movementAudioSource.loop = true;
            movementAudioSource.Play();
        }
    }

    // Stop all player movement sounds
    public void StopMovementSounds() {
        movementAudioSource.Stop();
    }

    public void PlayOutOfBreathSound()
    {
        if (!(breathingAudioSource.clip == outOfBreathSound && breathingAudioSource.isPlaying)) {
            breathingAudioSource.clip = outOfBreathSound;
            breathingAudioSource.loop = true;
            breathingAudioSource.Play();
        }
    }

    public void StopOutOfBreathSound() {
        breathingAudioSource.loop = false;
    }
}
