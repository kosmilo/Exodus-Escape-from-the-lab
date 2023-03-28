using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CellDoorSwitch : MonoBehaviour
{
    [SerializeField] List<CellBars> bars = new List<CellBars>();
    [SerializeField] List<Door> doors = new List<Door>();

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchInteraction()
    {
        foreach (CellBars b in bars)
        {
            b.DoorInteraction();
        }
        foreach (Door d in doors)
        {
            d.DoorInteraction();
        }

        animator.SetBool("Pulled", !animator.GetBool("Pulled"));
    }
}

