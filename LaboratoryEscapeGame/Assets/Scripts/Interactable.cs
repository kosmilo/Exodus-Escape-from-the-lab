using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent interact;

    // Invoke the event set in object's inspector
    public void Interact()
    {
        interact.Invoke();
    }
}
