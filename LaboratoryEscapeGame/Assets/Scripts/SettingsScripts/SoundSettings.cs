using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider uiSfxSlider;
    [SerializeField] Slider gameSfxSlider;

    public const string MIXER_MUSIC = "musicVolume";
    public const string MIXER_UI = "uiSfxVolume";
    public const string MIXER_GAME = "gameSfxVolume";

    private void Awake() {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        uiSfxSlider.onValueChanged.AddListener(SetUiVolume);
        gameSfxSlider.onValueChanged.AddListener(SetGameVolume);
    }

    private void Start() {
        musicSlider.value = PlayerPrefs.GetFloat(SoundManager.MUSIC_KEY, 0.8f);
        uiSfxSlider.value = PlayerPrefs.GetFloat(SoundManager.UISFX_KEY, 0.8f);
        gameSfxSlider.value = PlayerPrefs.GetFloat(SoundManager.GAMESFX_KEY, 0.8f);
    }

    void SetMusicVolume(float volume) {
        Debug.Log("Value changed");
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }

    void SetUiVolume(float volume) {
        Debug.Log("Value changed");
        mixer.SetFloat(MIXER_UI, Mathf.Log10(volume) * 20);
    }

    void SetGameVolume(float volume) {
        Debug.Log("Value changed");
        mixer.SetFloat(MIXER_GAME, Mathf.Log10(volume) * 20);
    }

    private void OnDisable() {
        PlayerPrefs.SetFloat(SoundManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(SoundManager.UISFX_KEY, uiSfxSlider.value);
        PlayerPrefs.SetFloat(SoundManager.GAMESFX_KEY, gameSfxSlider.value);
    }
}
