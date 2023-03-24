using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBars : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;
    [SerializeField]
    private float speed = 1f;
    [SerializeField] bool startOpen;

    [Header("Sliding Configs")]
    [SerializeField]
    private Vector3 slideDirection = Vector3.back;
    [SerializeField]
    private float slideAmount = 3.4f;

    private Vector3 startRotation;
    private Vector3 startPosition;

    private Coroutine animationCoroutine;

    GameObject player;

    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenClip;
    [SerializeField] AudioClip doorClosedClip;

    private void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        // audioSource = GetComponent<AudioSource>();
        // audioSource.loop = false;
    }

    private void Start()
    {
        if (startOpen)
        {
            Open();
        }
    }

    // Open the door if it currently isn't open
    // If it's currently in the middle of the animation coroutine, stop the coroutine
    // If the door is a rotating door, start the coroutine based on dot
    public void Open()
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(DoSlidingOpen());
            
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = doorOpenClip;
                audioSource.Play(); // Play door sound effect
            }
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = startPosition + slideAmount * slideDirection;
        Vector3 StartPosition = transform.position;

        float time = 0;
        isOpen = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(StartPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    // Close the door
    // If the door is open and the coroutine is happening, stop the coroutine
    // If the door is a rotating door, start the closing coroutine
    public void Close()
    {
        if (isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(DoSlidingClose());

            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = doorClosedClip;
                audioSource.Play(); // Play door sound effect
            }
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = startPosition;
        Vector3 StartPosition = transform.position;
        float time = 0;

        isOpen = false;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(StartPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

    }

    // Method to be called when player is interacting with the door
    // If the door is closed and not in the middle of a coroutine, open it and vice versa
    public void DoorInteraction()
    {
        Debug.Log("Interact with the door");
        if (!isOpen)
        {
            Open();
        }
        else if (isOpen)
        {
            Close();
        }

        if (GetComponent<Interactable>() != null)
        {
            GetComponent<Interactable>().interactionText = isOpen ? "Close" : "Open";
        }
    }
}
