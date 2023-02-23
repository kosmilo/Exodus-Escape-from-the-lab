using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item item;
    public Image slotImage;
    private Image spriteImage;
    public bool active; 
    public GameObject parentSlot;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        parentSlot = transform.parent.gameObject;
        slotImage = parentSlot.GetComponent<Image>();
        UpdateItem(null);
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void ChangeSlotType(Sprite sprite, bool activity)
    {
        slotImage.sprite = sprite;
        active = activity;
    }
}
