using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Instance that can be accessed from other scripts
    [SerializeField] public AudioMixer mixer;
    [SerializeField] AudioSource musicSource;

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
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Load and set volume settings from player prefs
    void LoadVolume() {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.8f);
        float uiVolume = PlayerPrefs.GetFloat(UISFX_KEY, 0.8f);
        float gameVolume = PlayerPrefs.GetFloat(GAMESFX_KEY, 0.8f);

        mixer.SetFloat(SoundSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(SoundSettings.MIXER_UI, Mathf.Log10(uiVolume) * 20);
        mixer.SetFloat(SoundSettings.MIXER_GAME, Mathf.Log10(gameVolume) * 20);
    }

    // Start the music fade coroutine
    public void MusicFadeToGame(float delay) {
        StartCoroutine(FadeMusicInOut(delay));
    }

    // Fade the music out, then wait for delay and fade the music back in
     IEnumerator FadeMusicInOut(float delay) {
        while (musicSource.volume > 0) {
            musicSource.volume -= Time.deltaTime * .5f;
            yield return 0;
        }

        yield return new WaitForSeconds(delay);

        while (musicSource.volume < 1)
        {
            musicSource.volume += Time.deltaTime * .2f;
            yield return 0;
        }

        yield return null;
    }
}
