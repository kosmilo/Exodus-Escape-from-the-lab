using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableNoteObject : MonoBehaviour
{
    ReadableNote readableNoteUI;
    [TextArea(15,20)]
    [SerializeField] string noteText;

    void Start() {
        readableNoteUI = FindObjectOfType<ReadableNote>();
    }

    public void ShowTextInNote() {
        FindObjectOfType<PlayerMovement>().allowedToMove = false;
        readableNoteUI.ShowNote(noteText);
    }
}
