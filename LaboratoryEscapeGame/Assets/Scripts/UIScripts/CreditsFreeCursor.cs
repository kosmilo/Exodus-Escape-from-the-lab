using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsFreeCursor : MonoBehaviour
{
    [SerializeField] GameObject fade;

    void Start()
    {
        // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        fade.GetComponent<Animator>().Play("SceneFadeInCredits");
    }
}
