using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoorSwitch : MonoBehaviour
{
    [SerializeField]
    private List<Door> doors = new List<Door>();

    public void SwitchInteraction()
    {
        foreach (Door d in doors)
        {
            d.DoorInteraction();
        }
    }
}

