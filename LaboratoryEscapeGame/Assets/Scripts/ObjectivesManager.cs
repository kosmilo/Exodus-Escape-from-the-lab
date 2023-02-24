using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesManager : MonoBehaviour
{
    TextMeshProUGUI objectivesText;
    Dictionary<string, string> objectives = new Dictionary<string, string>() 
    {
        {"WayOut", "Find a way out"},
        {"Elevator", "Fix the elevator"}
    };


    void Start()
    {
        objectivesText = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateObjectives("WayOut");
    }

    public void UpdateObjectives(string objective) {
        objectivesText.text = "- " + objectives[objective];
    }
}
