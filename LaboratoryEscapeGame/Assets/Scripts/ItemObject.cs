using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    Inventory inv;
    [SerializeField] int itemId;

    public void PickUpItem() {
        inv = GameObject.Find("pref_Player").GetComponent<Inventory>();
        bool pickedUp = inv.GiveItem(itemId); // Add item to inventory

        // If item was picked up successfully, destroy this game object
        if (pickedUp) {
            Destroy(gameObject);
        }
    }
}
