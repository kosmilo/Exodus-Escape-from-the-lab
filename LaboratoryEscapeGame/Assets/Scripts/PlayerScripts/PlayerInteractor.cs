using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InteractionIndicator intIndicator;
    RaycastHit hit;
    [SerializeField] LayerMask ignoreRayMask;

    void Awake() {
        intIndicator = FindObjectOfType<InteractionIndicator>();
    }

    void Update()
    {
        // Do a raycast in front of the player when mause button is pressed, 
        // if hit object has [interactable] script, call a [Interact]

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5, ~ignoreRayMask)) {
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                string text = hit.collider.gameObject.GetComponent<Interactable>().interactionText;
                intIndicator.UpdateIndicator(text);
                if (Input.GetMouseButtonDown(0)) {
                    hit.collider.gameObject.GetComponent<Interactable>().Interact();
                }
            }
            else {
                intIndicator.UpdateIndicator(" ");
            }
        } else {
            intIndicator.UpdateIndicator(" ");
        }
    }
}
