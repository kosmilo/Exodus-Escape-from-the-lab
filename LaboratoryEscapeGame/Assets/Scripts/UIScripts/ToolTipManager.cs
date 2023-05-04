using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ToolTipManager : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    CanvasGroup itemCanvas;
    public Item toolTipItem;

    void Start()
    {
        itemCanvas = GetComponent<CanvasGroup>();
        HideToolTip();
    }

    public void UpdateToolTip(Item item)
    {
        toolTipItem = item;
        itemName.text = item.title;
        itemDescription.text = item.description;
        itemCanvas.alpha = 1;
    }

    public void HideToolTip()
    {
        itemCanvas.alpha = 0;
    }


}
