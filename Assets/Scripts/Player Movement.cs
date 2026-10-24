using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float groundDrag = 6f;

    [SerializeField] private Transform orientation;
    private Rigidbody rb;

    private bool isInTrigger;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    public UnityEvent switchCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ApplyDrag();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnEnterChair()
    {
        Debug.Log("Pressed Enter Chair");
        if (isInTrigger)
        {
            Debug.Log("Inside chair trigger");
            switchCamera.Invoke();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cockpit Chair"))
        {
            Debug.Log("Entered Chair Trigger");
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cockpit Chair"))
        {
            Debug.Log("Exited Chair Trigger");
            isInTrigger = false;
        }
    }
}
