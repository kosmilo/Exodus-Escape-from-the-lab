using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    GameObject cameraObj;
    [SerializeField] StaminaBar staminaBar;

    float horizontalInput, verticalInput, mouseXInput, mouseYInput, runInput;
    Vector3 cameraRotation, playerRotation, playerMovement; // camera rotation = up-down, player rotation = left-right
    float cameraXAxisClamp;
    float rotationSensitivity = 4f;

    float movementSpeed;
    bool staminaRegenState;
    float walkSpeed = 3f;
    float runSpeed = 6f;
    float maxStamina = 400f;
    float stamina;
    float staminaDrain = 1f;
    float staminaRegen = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraObj = transform.GetChild(0).gameObject;
        stamina = maxStamina;
    }

    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");
        runInput = Input.GetAxis("Run");

        HandleRotation();
        staminaBar.UpdateStamina((stamina/maxStamina));
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleRotation()
    {
        // Get the current rotation
        cameraRotation = cameraObj.transform.rotation.eulerAngles;
        playerRotation = transform.rotation.eulerAngles;

        // Change rotation based on inputs
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

    void HandleMovement()
    {
        // Check if player should be running and assign movement speed accordingly
        if(runInput > 0.5 && !staminaRegenState)
        {
            stamina -= staminaDrain;
            movementSpeed = runSpeed;
        }
        else
        {
            stamina += staminaRegen;
            movementSpeed = walkSpeed;
        }

        // Set the player's stamina regen state which stops the player from running after stamina reaches 0
        if(staminaRegenState && stamina > 50)
        {
            staminaRegenState = false;
            Debug.Log("Player movement stamina regen state: " + staminaRegenState);
        }
        else if(stamina < 1)
        {
            staminaRegenState = true;
            Debug.Log("Player movement stamina regen state: " + staminaRegenState);
        }
        

        // Get the direction player is moving towards based on player input and the direction player is currently facing
        // and multiply it with movement speed
        playerMovement = Vector3.ClampMagnitude((transform.forward * verticalInput) + (transform.right * horizontalInput), 1) * movementSpeed;
        playerMovement.y = rb.velocity.y;

        rb.velocity = playerMovement;
    }
}
