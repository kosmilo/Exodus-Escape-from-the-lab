using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrematoriumBox : MonoBehaviour
{
    CrematoriumPuzzleManager puzzleManager;
    bool cremationFinished;

    void Start() {
        puzzleManager = FindObjectOfType<CrematoriumPuzzleManager>();
    }

    public void LeverPulled() {
        if (!cremationFinished) {
            // play lever pull animation and check for completion
            GetComponent<Animator>().Play("PullLever");
        }
        
        cremationFinished = puzzleManager.CheckCompletion(); 

        // Change the interaction text
        if (cremationFinished) {
            transform.GetChild(0).GetChild(0).GetComponent<Interactable>().interactionText = "Open";
            transform.GetChild(0).GetChild(2).GetComponent<Interactable>().interactionText = "";
            transform.GetChild(0).GetChild(2).GetComponent<Interactable>().enabled = false;
        }
    }

    public void DoorInteraction() {
        // Play door open animation
        if (cremationFinished) {
            GetComponent<Animator>().Play("OpenDoor");
            // Remove interaction text
            transform.GetChild(0).GetChild(0).GetComponent<Interactable>().interactionText = "";
        }

    }
}
