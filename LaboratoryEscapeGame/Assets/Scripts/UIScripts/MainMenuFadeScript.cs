using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("ToMainMenu", true);
        Debug.Log("Main menu fade start is called");
    }
}
