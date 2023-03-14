using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hit;

    void Update()
    {
        // Do a raycast in front of the player when mause button is pressed, 
        // if hit object has [interactable] script, call a [Interact]
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact();
            }
        }
    }
}