using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : MonoBehaviour
{
    [SerializeField] GameObject emptySpace;

    void Start()
    {
        emptySpace = transform.parent.GetChild(8).gameObject;
    }

    // Switch places with [EmptySpace] gameobject if it is close enough
    public void SlideBlock()
    {
        if (Vector3.Distance(emptySpace.transform.position, transform.position) < 0.3)
        {
            Vector3 lastEmptySpacePosition = emptySpace.transform.position;
            emptySpace.transform.position = transform.position;
            transform.position = lastEmptySpacePosition;
        }
    }
}
