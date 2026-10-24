using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CockpitCamera : MonoBehaviour
{
    private Quaternion defaultRotation;
    private Vector3 lookLeftEuler;
    private Vector3 lookRightEuler;
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

    private bool lookingLeft = false;
    private bool lookingRight = false;

    private void Start()
    {
        defaultRotation = transform.rotation;
        defaultPosition = transform.position;
        rotateTo = defaultRotation;

        lookLeftEuler = defaultRotation.eulerAngles;
        lookLeftEuler.y -= rotateLeftBy;

        lookRightEuler = defaultRotation.eulerAngles;
        lookRightEuler.y += rotateRightBy;
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
        if (lookingLeft) return;

        if (lookingRight)
        {
            lookingRight = false;
            RotateStraight();
        }
        else
        {
            rotateTo = Quaternion.Euler(lookLeftEuler);
            lookingLeft = true;
        }

        Debug.Log("Rotate left");
    }
    public void RotateRight()
    {
        if (lookingRight) return;

        if (lookingLeft)
        {
            lookingLeft = false;
            RotateStraight();
        }
        else
        {
            rotateTo = Quaternion.Euler(lookRightEuler);
            lookingRight = true;
        }

        Debug.Log("Rotate right");
    }

    public void RotateUp()
    {
        if (rotateTo != defaultRotation) return;

        Vector3 newEuler = transform.rotation.eulerAngles;
        newEuler.x -= rotateUpBy;
        rotateTo = Quaternion.Euler(newEuler);
        Debug.Log("Rotate up");
    }

    public void RotateForward()
    {

    }

    public void RotateDown()
    {
        if (rotateTo != defaultRotation) return;

        Vector3 newEuler = transform.rotation.eulerAngles;
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
