using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    Animator animator;
    bool isElevatorFixed = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetElevatorToFixed() {
        isElevatorFixed = true;
    }

    public void OpenDoors() {
        if (isElevatorFixed) {
            animator.Play("Open");
        }
    }

    public void CloseDoors() {
        animator.Play("Close");
    }
}
