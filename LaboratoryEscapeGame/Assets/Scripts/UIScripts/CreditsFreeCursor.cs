using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsFreeCursor : MonoBehaviour
{
    void Start()
    {
        // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
