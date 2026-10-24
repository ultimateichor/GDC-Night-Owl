using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerOutsideMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [SerializeField] private Transform orientation;
    [SerializeField] private GameObject playerMesh;
    private Rigidbody rb;
    private float turnSmoothVelocity;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    [SerializeField] private Transform cameraTransform;
    public UnityEvent switchCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ApplyDrag();
        RotatePlayer();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void MovePlayer()
    {
        moveDirection = cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (horizontalVelocity.magnitude < moveSpeed || Vector3.Dot(horizontalVelocity, moveDirection) < 0f)
        {
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }

        Vector3 clampedVelocity = Vector3.ClampMagnitude(horizontalVelocity, moveSpeed);

        rb.linearVelocity = new Vector3(clampedVelocity.x, rb.linearVelocity.y, clampedVelocity.z);
    }

    private void ApplyDrag()
    {
        if (moveInput == Vector2.zero)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0f;
        }
    }

    private void RotatePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            playerMesh.transform.rotation = Quaternion.RotateTowards(playerMesh.transform.rotation, orientation.rotation, 0f);
        }
    }
}
