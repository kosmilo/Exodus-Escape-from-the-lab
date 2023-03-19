using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Collection of methods that are called from other scripts to play sounds

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource movementAudioSource;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip runSound;

    // If walk sound isn't playing, play walk sound
    public void PlayWalkingSound() {
        if (!(movementAudioSource.clip == walkSound && movementAudioSource.isPlaying)) { 
            movementAudioSource.Stop();
            movementAudioSource.clip = walkSound;
            movementAudioSource.loop = true;
            movementAudioSource.Play();
         }
    }

    // If run sound isn't playing, play run sound
    public void PlayRunningSound() {
        if (!(movementAudioSource.clip == runSound && movementAudioSource.isPlaying)) { 
            movementAudioSource.Stop();
            movementAudioSource.clip = runSound;
            movementAudioSource.loop = true;
            movementAudioSource.Play();
        }
    }

    // Stop all player movement sounds
    public void StopMovementSounds() {
        movementAudioSource.Stop();
    }
}
