using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesManager : MonoBehaviour
{
    TextMeshProUGUI objectivesText;

    void Start()
    {
        objectivesText = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateObjective("Find a way out");
    }

    public void UpdateObjective(string objective) {
        objectivesText.text = objective;
    }
}
