using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoors() {
        animator.Play("Open");
    }

    public void CloseDoors() {
        animator.Play("Close");
    }
}
