using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionIndicator : MonoBehaviour
{
    TextMeshProUGUI indicatorText;

    void Awake() {
        indicatorText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateIndicator(string text) {
        if (text.Length > 1) {
            indicatorText.text = "[" + text + "]";
        } else {
            indicatorText.text = "";
        }
    }
}
