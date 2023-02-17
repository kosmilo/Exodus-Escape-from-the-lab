using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleManager : MonoBehaviour
{
    GameObject[] slidingPieces = new GameObject[9];

    // Collect all sliding pieces to an array
    void Start()
    {
        for (int i = 0; i < slidingPieces.Length; i++)
        {
            slidingPieces[i] = transform.GetChild(i).gameObject;
        }

        Suffle();
    }

    // Change places of random pieces to suffle
    void Suffle()
    {
        for (int i = 0; i < 6; i++)
        {
            int randomIndex1 = Random.Range(0, slidingPieces.Length);
            int randomIndex2 = Random.Range(0, slidingPieces.Length);
            Vector3 rPosition1 = slidingPieces[randomIndex1].transform.position;
            Vector3 rPosition2 = slidingPieces[randomIndex2].transform.position;
            slidingPieces[randomIndex1].transform.position = rPosition2;
            slidingPieces[randomIndex2].transform.position = rPosition1;
        }
    }

    // Check if all pieces are in correct positions
    public void CheckForCompletion()
    {
        bool isCompleted = true;

        for (int i = 0; i < slidingPieces.Length - 1; i++) {
            isCompleted = isCompleted && slidingPieces[i].GetComponent<SlidingPiece>().CheckPlace();
        }
        Debug.Log("Puzzle complition: " + isCompleted);
    }
}
