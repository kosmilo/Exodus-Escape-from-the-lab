using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorEntered : MonoBehaviour
{
    [SerializeField] GameObject elevator;
    [SerializeField] Animator elevatorDoorsAnimator;
    [SerializeField] UnityEvent gameWon;
    [SerializeField] Vector3 targetPos;
    [SerializeField] float moveSpeed;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            elevatorDoorsAnimator.Play("Close"); // Close the elevator doors
            StartCoroutine(MoveElevator()); 
        }
    }

    IEnumerator MoveElevator() {
        yield return new WaitForSeconds(2f); // Wait a few seconds before starting
        gameWon.Invoke();

        while (elevator.transform.position != targetPos)
        {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return 0; // continue on next frame
        }

        yield return null; // Stop coroutine
    }
}
