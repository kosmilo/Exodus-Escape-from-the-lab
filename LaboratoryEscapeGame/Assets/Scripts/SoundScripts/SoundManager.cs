using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Instance that can be accessed from other scripts
    [SerializeField] public AudioMixer mixer;

    // Keys for player prefs
    public const string MUSIC_KEY = "musicVolume";
    public const string UISFX_KEY = "uiVolume";
    public const string GAMESFX_KEY = "gameVolume"; 

    private void Awake() {

        // Destroy this object if an instance exists already, otherwise set this script as the instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadVolume();
    }

    // Load volume settings from player prefs
    void LoadVolume() {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.8f);
        float uiVolume = PlayerPrefs.GetFloat(UISFX_KEY, 0.8f);
        float gameVolume = PlayerPrefs.GetFloat(GAMESFX_KEY, 0.8f);

        mixer.SetFloat(SoundSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(SoundSettings.MIXER_UI, Mathf.Log10(uiVolume) * 20);
        mixer.SetFloat(SoundSettings.MIXER_GAME, Mathf.Log10(gameVolume) * 20);
    }
}
