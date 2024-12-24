using UnityEngine;
using UnityEngine.InputSystem;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float thrustForce = 10f;

    [SerializeField] private InputActionReference pitchInputRef;
    [SerializeField] private InputActionReference rollInputRef;
    [SerializeField] private InputActionReference yawInputRef;
    [SerializeField] private InputActionReference thrustInputRef;

    private void OnEnable()
    {
        // Enable the input actions
        pitchInputRef.action.Enable();
        rollInputRef.action.Enable();
        yawInputRef.action.Enable();
        thrustInputRef.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        pitchInputRef.action.Disable();
        rollInputRef.action.Disable();
        yawInputRef.action.Disable();
        thrustInputRef.action.Disable();
    }

    void FixedUpdate()
    {
        // Read input values for pitch, roll, and yaw as Vector2
        Vector2 pitchInput = pitchInputRef.action.ReadValue<Vector2>();
        Vector2 rollInput = rollInputRef.action.ReadValue<Vector2>();
        Vector2 yawInput = yawInputRef.action.ReadValue<Vector2>();
        
        // Read input value for thrust as float
        float thrustInput = thrustInputRef.action.ReadValue<float>();

        // Handle rotation based on input values for pitch, roll, and yaw
        HandleRotation(pitchInput, rollInput, yawInput);

        // Apply thrust force based on input value
        ApplyThrust(thrustInput);
    }

    private void HandleRotation(Vector2 pitchInput, Vector2 rollInput, Vector2 yawInput)
    {
        // Calculate rotation based on input as Vector2
        float pitchRotation = -pitchInput.y * rotationSpeed * Time.deltaTime;
        float yawRotation = yawInput.x * rotationSpeed * Time.deltaTime;
        float rollRotation = -rollInput.x * rotationSpeed * Time.deltaTime;

        // Apply rotation to the player's orientation
        transform.Rotate(pitchRotation, yawRotation, rollRotation);
    }

    private void ApplyThrust(float thrustInput)
    {
        // Apply thrust force based on input value
        Vector3 thrust = transform.forward * thrustInput * thrustForce * Time.deltaTime;
        transform.Translate(thrust, Space.World);
    }
}
