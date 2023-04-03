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
        // play lever pull animation and check for completion
        GetComponent<Animator>().Play("PullLever");
        cremationFinished = puzzleManager.CheckCompletion(); 

        // Change the door interaction text
        if (cremationFinished) {
            transform.GetChild(0).GetChild(0).GetComponent<Interactable>().interactionText = "Open";
        }
    }

    public void DoorInteraction() {
        // Play door open animation
        if (cremationFinished) {
            GetComponent<Animator>().Play("OpenDoor");
        }

        // Remove interaction text
        transform.GetChild(0).GetChild(0).GetComponent<Interactable>().interactionText = "";
    }
}
