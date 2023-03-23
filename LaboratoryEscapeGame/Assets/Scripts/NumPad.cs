using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class NumPad : MonoBehaviour
{
    [SerializeField]
    private int[] correctCombination;
    private int[] enteredCombination;
    public List<NumPadButton> buttons;
    private int digitsEntered;
    [SerializeField]
    private int maxDigits = 4;
    [SerializeField]
    UnityEvent unlock;

    private void Awake()
    {
        enteredCombination = new int[correctCombination.Length];
        digitsEntered = 0;

        buttons = this.GetComponentsInChildren<NumPadButton>().ToList();
    }

    public void OnButtonPress(int buttonNumber)
    {

        Debug.Log("Pressed button" + buttonNumber);

        // Add the number of the button pressed to the entered combination
        enteredCombination[digitsEntered] = buttonNumber;
        digitsEntered++;

        // Check if the max amount of digits have been entered
        if (digitsEntered == maxDigits)
        {
            // Check if entered code matches correct code
            if (enteredCombination.SequenceEqual(correctCombination))
            {
                Unlock();

            }
            else
            {
                ResetEnteredCode();
            }
        }
        
    }

    public void ResetEnteredCode()
    {
        digitsEntered = 0;
        enteredCombination = new int[correctCombination.Length];

        // Flash red light

        Debug.Log("Incorrect code");
    }

    public void Unlock()
    {

        unlock.Invoke();

        Debug.Log("Correct code");

        // Flash green light
    }
}
