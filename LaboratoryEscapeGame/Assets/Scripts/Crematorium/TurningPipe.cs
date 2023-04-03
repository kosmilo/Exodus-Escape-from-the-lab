using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPipe : MonoBehaviour
{
    [SerializeField] public int pipeType; // What kind of pipe, used when checking correct rotations

    private void Start() {
        // Give the pipe a random rotation
        transform.Rotate(new Vector3(0, 0, Random.Range(1, 4) * 90));
    }

    public void RotatePipe() {
        StartCoroutine(Rotation());
    }

    IEnumerator Rotation() {
        transform.Rotate(new Vector3(0, 0, 90));

        yield return null; // Stop coroutine
    }
}
