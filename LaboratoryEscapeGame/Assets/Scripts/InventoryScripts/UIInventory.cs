using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public Inventory inventory;
    public Sprite inactiveSlot;
    public Sprite activeSlot;
    public GameObject slotPrefab;
    public Transform slotPanel;
    public UIItem activeItem;
    public int numberOfSlots = 8;
    public KeyCode[] slotKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8 };

    private void Update()
    {
        // Check if any of the number keys are pressed
        for (int i = 0; i < slotKeys.Length; i++)
        { 
            // Activate inventory slot if a key is pressed
            if (Input.GetKeyDown(slotKeys[i])) { ActivateOneSlot(i); }   
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }

        // If player clicks use currently activated item
        if (Input.GetMouseButtonDown(0))
        {
            if (activeItem != null && activeItem.item != null && activeItem.active)
            {
                Debug.Log("Attempted to use item" + activeItem);
                inventory.ItemUsage(activeItem.item.id);
            }
        }
    }


    public void UpdateSlot(int slot, Item item)
    {
            uIItems[slot].UpdateItem(item);        
    }

    // Activate slot
    public void ActivateOneSlot(int slot)
    {
        // Activate (or deactivate if already activate) selected slot
        uIItems[slot].ChangeSlotType((!(uIItems[slot].active) ? activeSlot : inactiveSlot), !(uIItems[slot].active));
        activeItem = uIItems[slot];

        // Deactivate all other slots
        for (int i = 0; i < uIItems.Count; i++)
        {
            if (i == slot)
            {
                continue;
            }
            else
            {
                uIItems[i].ChangeSlotType(inactiveSlot, false);
            }
        }
    }

    public void AddNewItem(Item item)
    {
        //if there is already a uiitem with the same item, then
        //update count of uiitem
        //if there isnt a uiitem with the same item yet, then
        //add new item
        int index = uIItems.FindIndex(i => i.item == item);
        if (index != -1)
        {
            uIItems[index].UpdateCount(1);
            Debug.Log("Added count of item");
        }
        else
        {
            UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
            Debug.Log("Added new item");
        }
    }


    public void RemoveItem(Item item)
    {
        
        foreach(UIItem u in uIItems)
        {
            if(u.item == item)
            {
                u.UpdateCount(-1);
                Debug.Log("Removed count");
                if (u.itemCount == 0)
                {
                    UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
                    // uIItems.Remove(u);
                    Debug.Log("Removed " + u);
                }
            }
        }
    }

    public void DropItem()
    {
        for (int i = 0; i < uIItems.Count; i++)
        {
            if (uIItems[i].active == true && uIItems[i].item != null)
            {
                inventory.DropItemObject(uIItems[i].item.id);
                Debug.Log("Dropped item");
            }
        }
    }
}
