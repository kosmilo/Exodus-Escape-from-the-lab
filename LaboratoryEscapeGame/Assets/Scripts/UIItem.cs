using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItem : MonoBehaviour
{
    public Item item;
    public Image slotImage;
    private Image spriteImage;
    public bool active; 
    public GameObject parentSlot;
    public int itemCount;
    [SerializeField] TextMeshProUGUI counterText;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        parentSlot = transform.parent.gameObject;
        slotImage = parentSlot.GetComponent<Image>();
        UpdateItem(null);
        itemCount = 0;
        UpdateCount(0);
    }

    public void UpdateCount(int count)
    {
        itemCount += count;
        counterText.text = itemCount.ToString();
        if (itemCount > 1)
        {
            counterText.enabled = true;
        }
        else
        {
            counterText.enabled = false;
        }
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            itemCount = 1;
        }
        else
        {
            spriteImage.color = Color.clear;
            itemCount = 0;
        }
    }

    public void ChangeSlotType(Sprite sprite, bool activity)
    {
        slotImage.sprite = sprite;
        active = activity;
    }
}
