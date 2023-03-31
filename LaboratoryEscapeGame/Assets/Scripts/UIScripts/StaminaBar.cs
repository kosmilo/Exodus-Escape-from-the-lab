using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Image staminaBar;

    void Awake() {
        staminaBar = GetComponent<Image>();
    }

    // Set stamina bar width
    public void UpdateStamina(float barWidth) {
        staminaBar.fillAmount = barWidth;
    }
}
