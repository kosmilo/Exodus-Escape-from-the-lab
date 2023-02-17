using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;


    private void Start()
    {
        GiveItem(12);
        GiveItem(11);
        GiveItem(10);
        GiveItem(9);
        GiveItem(1);
        GiveItem(2);
        GiveItem(4);
        GiveItem(5);
        GiveItem(8);
    }

    public void GiveItem(int id) // give player an item by id
    {
        int amountOfItems = 0;
        foreach (UIItem u in inventoryUI.uIItems)
        {
            if(u.item != null)
            {
                amountOfItems++;
            }
        }
        if(amountOfItems < inventoryUI.numberOfSlots)
        {
            Item itemToAdd = itemDatabase.GetItem(id);
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added item: " + itemToAdd.title);
        }
        else
        {
            Debug.Log("Inventory full, item not added");
        }

    }

    public void GiveItem(string itemName) // give player an item by title
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id) // check if the item is in inventory by id
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id) // if the item is in inventory, remove it by id
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Removed item: " + itemToRemove.title);
        }
    }
}
