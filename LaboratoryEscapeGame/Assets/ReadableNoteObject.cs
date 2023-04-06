using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableNoteObject : MonoBehaviour
{
    ReadableNote readableNoteUI;
    [SerializeField] string noteText;

    void Start() {
        readableNoteUI = FindObjectOfType<ReadableNote>();
    }

    public void ShowTextInNote() {
        FindObjectOfType<PlayerMovement>().allowedToMove = false;
        readableNoteUI.ShowNote(noteText);
    }
}
