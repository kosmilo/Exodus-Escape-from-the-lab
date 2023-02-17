using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public Inventory inventory;
    public Sprite inactiveSlot;
    public Sprite activeSlot;
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 8;
    public KeyCode[] slotKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8 };

    private void Awake()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        
    }

    private void Update()
    {
        for (int i = 0; i < slotKeys.Length; i++)
        { 
            if (Input.GetKeyDown(slotKeys[i]))
            {
                ActivateOneSlot(i);
            }   
        }
    }


    public void UpdateSlot(int slot, Item item)
    {
            uIItems[slot].UpdateItem(item);        
    }

    public void ActivateOneSlot(int slot)
    {
        uIItems[slot].ChangeSlotType(activeSlot, true);
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
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
        foreach(UIItem u in uIItems)
        {
            if(u.item == item)
            {
                uIItems.Remove(u);
                Debug.Log("Removed " + u);
            }
        }
    }
}
