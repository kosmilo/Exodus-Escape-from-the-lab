using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NumPadButton : MonoBehaviour
{
    [SerializeField]
    private int number;
    private NumPad numpad;

    private void Awake()
    {
        number = int.Parse(this.gameObject.name);
        numpad = transform.parent.GetComponent<NumPad>();
    }

    public void ButtonPress()
    {
        numpad.OnButtonPress(number);
    }
}
