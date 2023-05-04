using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySettings : MonoBehaviour
{
    [SerializeField] Slider mouseSensSlider;
    PlayerMovement playerMovement;
    [SerializeField] SettingsData settingsData;
    bool isSetUp;


    private void Start() {
        settingsData = GameObject.FindObjectOfType<SettingsData>();

        // Find the player movement script and add a method from it as a listener
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null) {
            mouseSensSlider.onValueChanged.AddListener(playerMovement.SetMouseSens);
        }

        mouseSensSlider.value = settingsData.mouseSens;
        isSetUp = true;
    }
    
    // Save the slider value to player prefs when this object is disabled
    private void OnDisable() { 
        if (isSetUp)
        {
            settingsData.mouseSens = mouseSensSlider.value;
        }
    }
}
