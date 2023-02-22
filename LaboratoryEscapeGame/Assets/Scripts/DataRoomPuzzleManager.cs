using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRoomPuzzleManager : MonoBehaviour
{
    [SerializeField] DataRoomSlidePuzzle[] puzzles;
    [SerializeField] Animator boxAnimator;

    // Check if all puzzles are completed
    public void CheckPuzzleCompletion() {
        bool puzzleFinished = true;

        foreach (DataRoomSlidePuzzle puzzle in puzzles) {
            puzzleFinished = puzzleFinished && puzzle.isCompleted;
        }

        if (puzzleFinished) {
            boxAnimator.Play("openPartBox");
            Debug.Log("Dataroom completed");
        }
    }
}
