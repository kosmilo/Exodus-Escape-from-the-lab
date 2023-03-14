using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionSettings : MonoBehaviour
{
    [SerializeField] List<ResItem> resolutions = new List<ResItem>(); // List of possible resolutions
    int selectedResolution; // Index of selected resolution
    [SerializeField] TMP_Text resolutionText;

    // Change selected resolution with arrow buttons
    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    // Update resolution text in options menu
    public void UpdateResLabel()
    {
        resolutionText.text = $"{resolutions[selectedResolution].horizontal} x {resolutions[selectedResolution].vertical}";
    }

    // Apply graphics only when pressing save
    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, true);
    }
}

// Item that stores a resolution value
[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}