using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inventory in player game object
public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    [SerializeField] List<GameObject> itemPrefs = new List<GameObject>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;

    private void Start()
    {
        GiveItem(1);
        GiveItem(2);
        GiveItem(2);
        GiveItem(11);
        GiveItem(11);
        GiveItem(11);
    }

    public bool GiveItem(int id) // give the player an item by id (return bool to know if item game object should be restroyed)
    {
        // Check amount of items in UI inventory
        int amountOfItems = 0;
        bool isAlreadyInInventory = false;

        foreach (UIItem u in inventoryUI.uIItems)
        {
            if(u.item != null)
            {
                amountOfItems++;
                if (u.item.id == id)
                {
                    isAlreadyInInventory = true;
                }
            }
            
        }

        // Check if there are empty slots in UI inventory
        if (amountOfItems < inventoryUI.numberOfSlots | isAlreadyInInventory)
        {
            Item itemToAdd = itemDatabase.GetItem(id);
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            // Debug.Log("Added item: " + itemToAdd.title);
            return true;
        }
        else
        {
            Debug.Log("Inventory full, item not added");
            return false;
        }

    }

    /*
    public void GiveItem(string itemName) // give player an item by title
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }
    */

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

    public void DropItemObject(int id) {
        Vector3 instansiationPos = transform.position + transform.forward * 2;
        instansiationPos.y = transform.position.y + 0.4f;
        Instantiate(itemPrefs[id], instansiationPos, Quaternion.identity);
        Debug.Log("Dropped item " + id);
        RemoveItem(id);
    }
}
