using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public void ButtonSound() {
        SoundManager.Instance.transform.GetComponentInChildren<UISFXManager>().PlayButtonSound();
    }
}
