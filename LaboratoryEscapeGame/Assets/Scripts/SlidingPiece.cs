using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPiece : MonoBehaviour
{
    [SerializeField] GameObject emptySpace;
    Vector3 correctPosition;
    float moveSpeed = 2f;

    void Awake()
    {
        emptySpace = transform.parent.GetChild(8).gameObject;
        correctPosition = transform.position;
    }

    // Switch places with [EmptySpace] gameobject if it is close enough
    public void SlidePiece()
    {
        if (Vector3.Distance(emptySpace.transform.position, transform.position) < 0.3)
        {
            Vector3 lastPosition = transform.position;
            StartCoroutine(Slide(emptySpace.transform.position)); // Start a coroutine that moves the block
            emptySpace.transform.position = lastPosition;
        }
    }

    public bool CheckPlace() { return transform.position == correctPosition; }
    
    // Slide piece to the empty space
    IEnumerator Slide(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return 0; // continue on next frame
        }

        // Check for completion
        if (transform.position == correctPosition)
        {
            transform.parent.GetComponent<SlidePuzzleManager>().CheckForCompletion();
        }
        yield return null; // Stop coroutine
    }
}
