using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] int itemId;

    public void PickUpItem() {
        Inventory inv = FindObjectOfType<Inventory>();
        bool pickedUp = inv.GiveItem(itemId); // Add item to inventory

        // If item was picked up successfully, destroy this game object
        if (pickedUp) {
            Destroy(gameObject);
        }
    }
}
