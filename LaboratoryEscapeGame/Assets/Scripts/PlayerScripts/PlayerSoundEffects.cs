using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource runAudioSource;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip runSound;

    // If walk sound isn't playing, play walk sound
    public void PlayWalkingSound() {
        if (!(runAudioSource.clip == walkSound && runAudioSource.isPlaying)) { 
            runAudioSource.Stop();
            runAudioSource.clip = walkSound;
            runAudioSource.loop = true;
            runAudioSource.Play();
         }
    }

    // If run sound isn't playing, play run sound
    public void PlayRunningSound() {
        if (!(runAudioSource.clip == runSound && runAudioSource.isPlaying)) { 
            runAudioSource.Stop();
            runAudioSource.clip = runSound;
            runAudioSource.loop = true;
            runAudioSource.Play();
        }
    }

    public void StopMovementSounds() {
        runAudioSource.Stop();
    }
}
