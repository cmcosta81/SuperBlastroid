using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpActionInput;
    public bool isOnGround => Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f), Vector3.down, 2.0f);
    
    private XROrigin xRRig;
    private Rigidbody playerRb;
    private CapsuleCollider capsuleCollider;
    
    // Some Physics stuff    
    public float jumpForce = 100.0f;

    // Start is called before the first frame update
    private void Start()
    {
        jumpActionInput.action.performed += OnJump;
    }
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        xRRig = GetComponent<XROrigin>();
        capsuleCollider = GetComponent<CapsuleCollider>();
                    
    }

    // Update is called once per frame
    void Update()
    {
        var center = xRRig.CameraInOriginSpaceHeight;
        capsuleCollider.height = Mathf.Clamp(xRRig.CameraInOriginSpaceHeight, 1.0f, 3.0f);
        capsuleCollider.center = new Vector3(0, capsuleCollider.height / 2, 0);               
    }

    void OnJump(InputAction.CallbackContext obj)
    {
        //if (!isOnGround) return;
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);              
    }
}