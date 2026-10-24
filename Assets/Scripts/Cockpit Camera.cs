using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CockpitCamera : MonoBehaviour
{
    private Quaternion defaultRotation;
    private Quaternion rotateTo;
    private Vector3 defaultPosition;
    public UnityEvent switchCamera;
    public UnityEvent topDown;
    [SerializeField] private float rotateLeftBy = 10f;
    [SerializeField] private float rotateRightBy = 10f;
    [SerializeField] private float rotateUpBy = 10f;
    [SerializeField] private float rotateDownBy = 10f;
    [SerializeField] private float rotateTime = 5f;
    [SerializeField] private float angleThreshold = 1.0f;

    private void Start()
    {
        defaultRotation = transform.rotation;
        defaultPosition = transform.position;
        rotateTo = defaultRotation;
    }

    private void Update()
    {
        float angleDifference = Quaternion.Angle(transform.rotation, rotateTo);

        if (angleDifference > angleThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, rotateTime * Time.deltaTime);
        }
        else
        {
            transform.rotation = rotateTo;
        }
    }

    public void OnLeaveChair()
    {
        Debug.Log("Leave Chair");
        switchCamera.Invoke();
    }

    public void OnTopDown()
    {
        Debug.Log("Top Down");
        topDown.Invoke();
    }

    public void RotateLeft()
    {
        if (rotateTo != defaultRotation) return;

        Vector3 newEuler = defaultRotation.eulerAngles;
        newEuler.y -= rotateLeftBy;
        rotateTo = Quaternion.Euler(newEuler);
        Debug.Log("Rotate left");
    }
    public void RotateRight()
    {
        if (rotateTo != defaultRotation) return;

        Vector3 newEuler = defaultRotation.eulerAngles;
        newEuler.y += rotateRightBy;
        rotateTo = Quaternion.Euler(newEuler);
        Debug.Log("Rotate right");
    }

    public void RotateUp()
    {
        if (rotateTo != defaultRotation) return;

        Vector3 newEuler = defaultRotation.eulerAngles;
        newEuler.x -= rotateUpBy;
        rotateTo = Quaternion.Euler(newEuler);
        Debug.Log("Rotate up");
    }

    public void RotateDown()
    {
        if (rotateTo != defaultRotation) return;

        Vector3 newEuler = defaultRotation.eulerAngles;
        newEuler.x += rotateUpBy;
        rotateTo = Quaternion.Euler(newEuler);
        Debug.Log("Rotate down");
    }

    public void RotateStraight()
    {
        if (rotateTo == defaultRotation) return;

        rotateTo = defaultRotation;
        Debug.Log("Rotate straight");
    }
}
