using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;

    void Start() {
        healthBar = GetComponent<Image>();
    }

    // Set health bar width
    public void UpdateHealth(float barWidth) {
        healthBar.fillAmount = barWidth;
    }
}
