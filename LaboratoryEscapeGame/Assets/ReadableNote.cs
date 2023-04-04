using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadableNote : MonoBehaviour
{
    TextMeshProUGUI noteText;
    Image noteBackground;

    // Start is called before the first frame update
    void Start()
    {
        noteText = GetComponentInChildren<TextMeshProUGUI>();
        noteBackground = GetComponent<Image>();
        HideNote();
    }

    public void ShowNote(string text) {
        if (!noteBackground.enabled) {
            noteBackground.enabled = true;
            noteText.text = text;
            Debug.Log("Note showed");

            StartCoroutine(NoteShowing());
        }
    }

    public void HideNote() {
        noteBackground.enabled = false;
        noteText.text = "";
        Debug.Log("Note hid");
    }

    IEnumerator NoteShowing() {
        yield return 5;
        Debug.Log("Note coroutine");

        while (true) {
            yield return 0;
            if (Input.GetMouseButtonDown(0)) {
                HideNote();
                break;
            }
        }
        Debug.Log("Stop coroutine");
        
        yield return null;
    }
}
