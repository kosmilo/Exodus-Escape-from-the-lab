using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InteractionIndicator interactionIndicator;
    RaycastHit hit;

    void Update()
    {
        // Do a raycast in front of the player when mause button is pressed, 
        // if hit object has [interactable] script, call a [Interact]

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5)) {
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                string text = hit.collider.gameObject.GetComponent<Interactable>().interactionText;
                interactionIndicator.UpdateIndicator(text);
                if (Input.GetMouseButtonDown(0)) {
                    hit.collider.gameObject.GetComponent<Interactable>().Interact();
                }
            }
            else {
                interactionIndicator.UpdateIndicator(" ");
            }
        } else {
            interactionIndicator.UpdateIndicator(" ");
        }
    }
}
