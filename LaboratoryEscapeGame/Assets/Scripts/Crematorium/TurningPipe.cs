using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPipe : MonoBehaviour
{
    [SerializeField] public int pipeType;
    [SerializeField] float rotationSpeed = 4;

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(1, 4) * 90));
    }

    public void RotatePipe()
    {
        StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        GetComponentInParent<CrematoriumPuzzleManager>().CheckCompletion();
        Debug.Log(transform.eulerAngles);

        yield return null; // Stop coroutine
    }
}
