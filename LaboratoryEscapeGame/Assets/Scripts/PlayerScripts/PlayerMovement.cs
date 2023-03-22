using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    GameObject cameraObj;
    PlayerSoundEffects playerSoundEffects;
    [SerializeField] StaminaBar staminaBar;

    // Key for player prefs
    const string SENS_KEY = "mouseSenstivity";

    float mouseXInput, mouseYInput, runInput;
    Vector2 movementInputs;
    Vector3 cameraRotation, playerRotation, playerMovement; // camera rotation = up-down, player rotation = left-right
    float cameraXAxisClamp;
    float rotationSensitivity;

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
        rotationSensitivity = PlayerPrefs.GetFloat(SENS_KEY, 5f); // Find the previous rotation sensitivity from player preffs

        // Get references to components
        rb = GetComponent<Rigidbody>();
        playerSoundEffects = GetComponent<PlayerSoundEffects>();
        cameraObj = transform.GetChild(0).gameObject;

        // Set stamina
        stamina = maxStamina;
    }

    void Update()
    {
        // Get input
        movementInputs.x = Input.GetAxis("Horizontal");
        movementInputs.y = Input.GetAxis("Vertical");
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
        // And play/stop running and walking sounds
        if (runInput > 0.5 && (Mathf.Abs(movementInputs.x) > 0.2f || Mathf.Abs(movementInputs.y)> 0.2f) && !staminaRegenState)
        {
            playerSoundEffects.PlayRunningSound();
            stamina -= staminaDrain;
            movementSpeed = runSpeed;
        }
        else
        {
            if (Mathf.Abs(movementInputs.x) > 0.2f || Mathf.Abs(movementInputs.y) > 0.2f) {
                playerSoundEffects.PlayWalkingSound();
            }
            else {
                playerSoundEffects.StopMovementSounds();
            }
            stamina += staminaRegen;
            movementSpeed = walkSpeed;
        }

        // Set the player's stamina regen state which stops the player from running after stamina reaches 0
        if(staminaRegenState && stamina > 50)
        {
            staminaRegenState = false;
        }
        else if(stamina < 1)
        {
            staminaRegenState = true;
        }
        staminaBar.UpdateStamina(stamina/maxStamina);

        // Play breathing sound
        if (stamina < 100) {
            playerSoundEffects.PlayOutOfBreathSound();
        }
        else if (stamina > 150) {
            playerSoundEffects.StopOutOfBreathSound();
        }
        
        // Clamp movement inputs if needed
        if (movementInputs.x + movementInputs.y > .7f) {
            movementInputs = Vector2.ClampMagnitude(movementInputs, 1);
        }

        // Get the direction player is moving towards based on player input and the direction player is currently facing
        // and multiply it with movement speed
        playerMovement = (transform.forward * movementInputs.y + transform.right * movementInputs.x) * movementSpeed;
        playerMovement.y = rb.velocity.y;

        rb.velocity = playerMovement;
    }

    // Set rotation sensitivity (added as a listener in MouseSensitivitySettings.cs)
    public void SetMouseSens(float value) {
        rotationSensitivity = value;
    }
}
