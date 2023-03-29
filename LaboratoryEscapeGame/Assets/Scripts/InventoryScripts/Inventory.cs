using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

// Inventory in player game object
public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    [SerializeField] List<GameObject> itemPrefs = new List<GameObject>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    public PlayerHealth playerHealth;
    private Item itemToBeUsed;
    RaycastHit hit;
    public Camera cam;

    private void Start()
    {
        GiveItem(1);
        GiveItem(2);
        GiveItem(2);
        GiveItem(3);
        GiveItem(3);
        GiveItem(4);
        GiveItem(4);
        GiveItem(5);
        GiveItem(5);
    }



    public bool GiveItem(int id) // give the player an item by id (return bool to know if item game object should be restroyed)
    {
        // Check amount of items in UI inventory
        int amountOfItems = 0;
        bool isAlreadyInInventory = false;

        foreach (UIItem u in inventoryUI.uIItems)
        {
            if (u.item != null)
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

    public void DropItemObject(int id)
    {
        Vector3 instansiationPos = transform.position + transform.forward * 2;
        instansiationPos.y = transform.position.y + 0.4f;
        Instantiate(itemPrefs[id], instansiationPos, Quaternion.identity);
        Debug.Log("Dropped item " + id);
        RemoveItem(id);
    }


    // this could probably be made shorter but for the sake of clarity (and my own sanity) i made it like this
    // by id, find the item to be used
    public void ItemUsage(int id)
    {
        itemToBeUsed = CheckForItem(id);

        // if the item is a consumable, find its heal stat and heal the player for that amount
        if (itemToBeUsed.canUseOnSelf)
        {
            int healAmount = itemToBeUsed.stats.First(i => i.Key == "Heal").Value;
            playerHealth.HealPlayer(healAmount);

            // Remove a count of the item from the inventory
            inventoryUI.RemoveItem(itemToBeUsed);

        }

        // if the item is a throwable (vial)
        if (itemToBeUsed.canThrow)
        {
            LabMonsterController hitMonster;

            // do a raycast to see if there's an enemy
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5) && hit.collider.gameObject.GetComponent<LabMonsterController>() != null)
            {
                hitMonster = hit.collider.gameObject.GetComponent<LabMonsterController>();

                // if the item is a slowing type, slow the enemy
                if (itemToBeUsed.stats.ContainsKey("Slow") && itemToBeUsed.stats.ContainsKey("SlowTime"))
                {
                    int slowAmount = itemToBeUsed.stats.First(i => i.Key == "Slow").Value;
                    int slowTime = itemToBeUsed.stats.First(i => i.Key == "SlowTime").Value;
                    hitMonster.SlowEnemy((float)slowAmount, (float)slowTime);
                }

                // if stunning type, stun
                if (itemToBeUsed.stats.ContainsKey("Stun"))
                {
                    int stunLength = itemToBeUsed.stats.First(i => i.Key == "Stun").Value;
                    hitMonster.StunEnemyFor((float)stunLength);
                }

                // Remove a count of the item from the inventory
                inventoryUI.RemoveItem(itemToBeUsed);
            }
        }
    }
}