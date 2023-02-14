using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public Image slotImage;
    public UIInventory uIInventory;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void ChangeSlotType(Sprite sprite)
    {
        slotImage.sprite = sprite;
    }

    public void UpdateSlot(Image slot)
    {
        this.slotImage = slot;
        if (this.slotImage != null)
        {
            slotImage.color = Color.white;
            slotImage.sprite = uIInventory.inactiveSlot;
        }
        else
        {
            slotImage.color = Color.clear;
        }
    } 

}
