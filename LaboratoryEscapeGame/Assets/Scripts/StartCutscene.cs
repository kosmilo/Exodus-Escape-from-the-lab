using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] GameObject hud;
    void Start() {
        hud.SetActive(false);
        Invoke("ControlToPlayer", 12.2f);
    }

    void ControlToPlayer() {
        hud.SetActive(true);
        GameObject.FindWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerMovement>().allowedToMove = true;
        Destroy(gameObject);
    }
}
