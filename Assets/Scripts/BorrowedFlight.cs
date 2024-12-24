using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BorrowedFlight : MonoBehaviour
{
    [SerializeField] float maxYRange;
    [SerializeField] float minYRange;
    [SerializeField] float xRange;
    [SerializeField] float ySpeed;
    [SerializeField] float xSpeed;
    [SerializeField] float positionPitchFactor = -5f;  // variable controlling the spaceship pitch based on current spaceship position on Y axis
    [SerializeField] float controlPitchFactor = -5f;  // variable controlling the additional spaceship pitch while pressing W/S or UP/DOWN arrow keys
    [SerializeField] float positionYawFactor = 5f;  // variable controlling  the spaceship yaw based on current spaceship position on X axis
    [SerializeField] float controlYawFactor = 5f;  // variable controlling the additional spaceship yaw while pressing A/D or LEFT/RIGHT arrow keys
    [SerializeField] float controlRollFactor = 1f;  // variable controlling the spaceship roll while pressing A/D or LEFT/RIGHT arrow keys
    [SerializeField] bool applySensitivityInput;
    [SerializeField] float inputSensitivity;
    [SerializeField] InputActionReference inputAction;
    [SerializeField] private InputActionReference controllerActionPitch;
    [SerializeField] private InputActionReference controllerActionRoll;
    [SerializeField] private InputActionReference controllerActionYaw;
    private Vector2 currentInputProgress;
    private Vector3 startingRotation;

    private void OnEnable()
    {
        inputAction.action.Enable();
    }

    private void OnDisable()
    {
        inputAction.action.Disable();
    }

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(10, 0, 0);
        startingRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
    }

    private void Update()
    {
        // Apply sensitivy smoothing to input system value
        if (applySensitivityInput)
        {
            SmoothInput();
        }
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        // Apply horizontal movement if input value is not 0
        if (CurrentVector2Value().x != 0)
        {
            float currentX = transform.localPosition.x;
            float clampedX = Mathf.Clamp(currentX + ThisFrameOffset(CurrentVector2Value().x, xSpeed), -xRange, xRange);
            transform.localPosition = new Vector3(clampedX, transform.localPosition.y, transform.localPosition.z);
        }
        // Apply horizontal movement if input value is not 0
        if (CurrentVector2Value().y != 0)
        {
            float currentY = transform.localPosition.y;
            float clampedY = Mathf.Clamp(currentY + ThisFrameOffset(CurrentVector2Value().y, ySpeed), minYRange, maxYRange);
            transform.localPosition = new Vector3(transform.localPosition.x, clampedY, transform.localPosition.z);
        }
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + CurrentVector2Value().y * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor + CurrentVector2Value().x * controlYawFactor;
        float roll = CurrentVector2Value().x * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void SmoothInput()
{
    currentInputProgress = Vector2.Lerp(currentInputProgress, inputAction.action.ReadValue<Vector2>(), inputSensitivity * Time.deltaTime);
}


    private Vector2 CurrentVector2Value()
    {
        if (applySensitivityInput)
        {
            return currentInputProgress;
        }
        else
        {
            return inputAction.action.ReadValue<Vector2>();
        }
    }

    private float ThisFrameOffset(float vectorValue, float speed)
    {
        return vectorValue * speed * Time.deltaTime;
    }
}
