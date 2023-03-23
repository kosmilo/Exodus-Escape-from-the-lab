using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoorSwitch : MonoBehaviour
{
    [SerializeField] List<CellBars> baars = new List<CellBars>();
    [SerializeField] List<Door> doors = new List<Door>();

    public void SwitchInteraction()
    {
        foreach (CellBars b in baars)
        {
            b.DoorInteraction();
        }
        foreach (Door d in doors)
        {
            d.DoorInteraction();
        }
    }
}

