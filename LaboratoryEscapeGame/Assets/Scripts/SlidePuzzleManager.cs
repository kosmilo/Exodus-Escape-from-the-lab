using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleManager : MonoBehaviour
{
    GameObject[] slidingBlocks = new GameObject[9];

    void Start()
    {
        for (int i = 0; i < slidingBlocks.Length; i++)
        {
            slidingBlocks[i] = transform.GetChild(i).gameObject;
        }

        Suffle();
    }

    void Suffle()
    {
        for (int i = 0; i < 6; i++)
        {
            int randomIndex1 = Random.Range(0, slidingBlocks.Length);
            int randomIndex2 = Random.Range(0, slidingBlocks.Length);
            Vector3 rPosition1 = slidingBlocks[randomIndex1].transform.position;
            Vector3 rPosition2 = slidingBlocks[randomIndex2].transform.position;
            slidingBlocks[randomIndex1].transform.position = rPosition2;
            slidingBlocks[randomIndex2].transform.position = rPosition1;
        }
    }

    public void CheckForCompletion()
    {
        bool isCompleted = true;

        for (int i = 0; i < slidingBlocks.Length - 1; i++) {
            isCompleted = isCompleted && slidingBlocks[i].GetComponent<SlidingBlock>().CheckPlace();
        }
        Debug.Log("Puzzle complition: " + isCompleted);
    }
}
