using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Lock : MonoBehaviour
{
    [SerializeField]
    private int[] requiredItems;
    private Inventory myInventory;
    private bool success;
    [SerializeField]
    UnityEvent lockOpen;
    AudioSource audioSource;
    [SerializeField]
    AudioClip unsuccessfulOpenClip;

    private void Awake()
    {
        myInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void LockInteraction()
    {
        // Execute only if player has all required items
        if (requiredItems.All(id => myInventory.CheckForItem(id) != null))
        {
            lockOpen.Invoke();
            Debug.Log("Player has all required items, lock opened");
        }
        else
        {
            Debug.Log("Player doesn't have all required items");

            audioSource.Stop();
            audioSource.clip = unsuccessfulOpenClip;
            audioSource.Play(); // Play sound effect
        }
    }
}
