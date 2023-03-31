using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUsageBar : MonoBehaviour
{
    Image usageBar;
    Image barHolder;
    float passedTime;
    bool inUse;
    float totalTime;
    public CanvasGroup canvas;

    private void Start()
    {
        usageBar = GetComponent<Image>();
        barHolder = GetComponentInParent<Image>();
        usageBar.fillAmount = 1;
        inUse = false;
        canvas.alpha = 0;
    }

    private void Update()
    {
        if (inUse)
        {
            UpdateTime();
        }
    }

    public void UpdateTime()
    {
        passedTime += Time.deltaTime;
  
        if (passedTime < totalTime)
        {
            // Change bar size accordingly
            usageBar.fillAmount = passedTime / totalTime;
        }
        else
        {
            // Stop and hide the bar
            Debug.Log("Hid the usage bar");
            canvas.alpha = 0;
            totalTime = 0;
            passedTime = 0;
            inUse = false;
        }
    }

    public void StartBar(float time)
    {
        passedTime = 0;
        totalTime = time;
        canvas.alpha = 1;
        Debug.Log("Enabled the usage bar");
        inUse = true;
    }
}
