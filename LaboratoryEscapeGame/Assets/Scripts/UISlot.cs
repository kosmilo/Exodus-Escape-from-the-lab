using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public Image slotImage;
    public Sprite activeSprite;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void ChangeSlotType(Sprite sprite)
    {
        slotImage.sprite = sprite;
    }

}
