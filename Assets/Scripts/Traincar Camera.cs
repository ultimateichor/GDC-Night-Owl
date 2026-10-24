using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TraincarCamera : MonoBehaviour
{
    public Transform orientation;
    private float xRotation;
    private float yRotation;
    private Vector2 mouseInput;

    [SerializeField] private float mouseSensitivity;

    private void OnLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }

    private void Update()
    {
        float mouseX = mouseInput.x * mouseSensitivity;
        float mouseY = mouseInput.y * mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
