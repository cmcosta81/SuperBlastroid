using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Vector2float : MonoBehaviour
{[Header("Object Reference")]
    [SerializeField]
    private InputActionReference MovementControl;

    private Vector2 rawInput;
    private float inputX, inputY;

    private void FixedUpdate()
    {
        // Storing the input into a Vector 2
        rawInput = MovementControl.action.ReadValue<Vector2>();

        // Storing the input into 2 floats
        inputX = MovementControl.action.ReadValue<Vector2>().x;
        inputY = MovementControl.action.ReadValue<Vector2>().y;
    }
}