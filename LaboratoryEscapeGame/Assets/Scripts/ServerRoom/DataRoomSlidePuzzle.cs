using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRoomSlidePuzzle : MonoBehaviour
{
    GameObject[] slidingPieces = new GameObject[9];
    DataRoomPuzzleManager dataRoomPuzzleManager;
    public bool isCompleted = false;
    [SerializeField] bool noShuffle;

    // Collect all sliding pieces to an array
    void Start()
    {
        dataRoomPuzzleManager = transform.parent.gameObject.GetComponent<DataRoomPuzzleManager>();
        for (int i = 0; i < slidingPieces.Length; i++)
        {
            slidingPieces[i] = transform.GetChild(0).GetChild(i).gameObject;
        }

        Suffle();
    }

    // Change places of random pieces to suffle
    void Suffle()
    {
        if (!noShuffle)
        {
            for (int i = 0; i < 4; i++)
            {
                int randomIndex1 = Random.Range(0, slidingPieces.Length);
                int randomIndex2 = Random.Range(0, slidingPieces.Length);
                Vector3 rPosition1 = slidingPieces[randomIndex1].transform.position;
                Vector3 rPosition2 = slidingPieces[randomIndex2].transform.position;
                slidingPieces[randomIndex1].transform.position = rPosition2;
                slidingPieces[randomIndex2].transform.position = rPosition1;
            }
        }
    }

    // Check if all pieces are in correct positions
    public void CheckSectionCompletion()
    {
        bool completion = true;

        for (int i = 0; i < slidingPieces.Length - 1; i++) {
            completion = completion && slidingPieces[i].GetComponent<SlidingPiece>().CheckPlace();
        }

        Debug.Log("Puzzle complition: " + completion);
        isCompleted = completion;
        if (isCompleted) {
            dataRoomPuzzleManager.CheckPuzzleCompletion();
        }
    }
}
