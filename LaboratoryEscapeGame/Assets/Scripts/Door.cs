using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;
    [SerializeField]
    private bool isRotatingDoor = true;
    [SerializeField]
    private float speed = 1f;

    [Header("Rotation configs")]
    [SerializeField]
    private float rotationAmount = 90f;
    [SerializeField]
    private float forwardDirection;

    [Header("Sliding Configs")]
    [SerializeField]
    private Vector3 slideDirection = Vector3.back;
    [SerializeField]
    private float slideAmount = 2.6f;

    private Vector3 startRotation;
    private Vector3 startPosition;
    private Vector3 forward;

    private Coroutine animationCoroutine;

    GameObject player;

    private void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        forward = transform.right;
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
    }

    // Open the door if it currently isn't open
    // If it's currently in the middle of the animation coroutine, stop the coroutine
    // If the door is a rotating door, start the coroutine based on dot
    public void Open(Vector3 userPosition)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            if (isRotatingDoor)
            {
                float dot = Vector3.Dot(forward, (userPosition - transform.position).normalized);
                animationCoroutine = StartCoroutine(DoRotationOpen(dot));
                Debug.Log("Dot: " + dot);
            }
            else
            {
                animationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    // Opening coroutine animation
    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion StartRotation = transform.rotation;
        Quaternion EndRotation;

        // Change the direction the door opens based on the user's position relative to the door
        // For some reason the door still always opens in only one direction but that might be because the interaction raycast stuff isn't implemented yet
        if (forwardAmount >= forwardDirection)
        {
            EndRotation = Quaternion.Euler(new Vector3(0, startRotation.y - rotationAmount, 0));
        }
        else
        {
            EndRotation = Quaternion.Euler(new Vector3(0, startRotation.y + rotationAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(StartRotation, EndRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = startPosition + slideAmount * slideDirection;
        Vector3 StartPosition = transform.position;

        float time = 0;
        isOpen = true;
        while(time < 1)
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

            if(isRotatingDoor)
            {
                animationCoroutine = StartCoroutine(DoRotationClose());
            }
            else
            {
                animationCoroutine = StartCoroutine(DoSlidingClose());
                    }
        }
    }

    // Closing coroutine animation
    private IEnumerator DoRotationClose()
    {
        Quaternion StartRotation = transform.rotation;
        Quaternion EndRotation = Quaternion.Euler(startRotation);

        isOpen = false;

        float time = 0;
        while ( time < 1)
        {
            transform.rotation = Quaternion.Slerp(StartRotation, EndRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = startPosition;
        Vector3 StartPosition = transform.position;
        float time = 0;

        isOpen = false;

        while(time < 1)
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
        if(!isOpen && animationCoroutine == null)
        {
            Open(player.transform.position);
        }
        if(isOpen && animationCoroutine == null)
        {
            Close();
        }
    }

    // Add buttons to inspector for opening and closing door 
    // I have no idea how this actually works (thanks ChatGPT!)
#if UNITY_EDITOR
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Door controller = (Door)target;

            if (GUILayout.Button("Open Door"))
            {
                controller.Open(Vector3.zero);
            }

           if (GUILayout.Button("Close Door"))
            {
                controller.Close();
            }
        }
    }
#endif
}



