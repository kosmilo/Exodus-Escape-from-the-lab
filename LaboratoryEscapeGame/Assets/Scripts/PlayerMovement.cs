using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    GameObject cameraObj;
    float horizontalInput, verticalInput, mouseXInput, mouseYInput, cameraXAxisClamp;
    Vector3 cameraRotation, playerRotation, velocity; // camera rotation = up-down, player rotation = left-right
    float walkSpeed = 3f;
    float runSpeed = 6f;
    float rotationSensitivity = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraObj = transform.GetChild(0).gameObject;

        // Lock and hide the cursor while game is playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");

        HandleRotation();
    }

    private void FixedUpdate()
    {
        // Get the direction player is moving towards based on player input and the direction player is currently facing and multiply it with movement speed
        velocity = Vector3.ClampMagnitude((transform.forward * verticalInput) + (transform.right * horizontalInput), 1) * walkSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }

    void HandleRotation()
    {
        cameraRotation = cameraObj.transform.rotation.eulerAngles;
        playerRotation = transform.rotation.eulerAngles;

        cameraRotation.x -= mouseYInput * rotationSensitivity;
        cameraRotation.z = 0;
        playerRotation.y += mouseXInput * rotationSensitivity;

        // Stop the camera from flipping when looking directly up or down
        cameraXAxisClamp -= mouseYInput * rotationSensitivity;
        if (cameraXAxisClamp > 90)
        {
            cameraXAxisClamp = 90;
            cameraRotation.x = 90;
        }
        else if (cameraXAxisClamp < -90)
        {
            cameraXAxisClamp = -90;
            cameraRotation.x = 270;
        }

        cameraObj.transform.rotation = Quaternion.Euler(cameraRotation);
        transform.rotation = Quaternion.Euler(playerRotation);
    }
}
