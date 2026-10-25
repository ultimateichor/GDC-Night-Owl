using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CockpitCamera : MonoBehaviour
{
    private Quaternion defaultRotation;
    private Vector3 defaultPosition;
    private Vector3 lookLeftEuler;
    private Vector3 lookRightEuler;
    private Quaternion rotateTo;
    private Quaternion zoomInRotation;
    private Vector3 moveTo;
    //public UnityEvent switchCamera;
    //public UnityEvent topDown;
    [SerializeField] private float rotateLeftBy = 10f;
    [SerializeField] private float rotateRightBy = 10f;
    [SerializeField] private float rotateTime = 5f;
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private float angleThreshold = 1.0f;
    [SerializeField] private float moveThreshold = 1.0f;

    private bool lookingLeft = false;
    private bool lookingRight = false;
    private bool zoomedIn = false;

    private void Start()
    {
        defaultRotation = transform.rotation;
        defaultPosition = transform.position;
        rotateTo = defaultRotation;
        moveTo = defaultPosition;
        zoomInRotation = transform.rotation;

        lookLeftEuler = defaultRotation.eulerAngles;
        lookLeftEuler.y -= rotateLeftBy;

        lookRightEuler = defaultRotation.eulerAngles;
        lookRightEuler.y += rotateRightBy;
    }

    private void Update()
    {
        if (!zoomedIn)
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
        else
        {
            float angleDifference = Quaternion.Angle(transform.rotation, zoomInRotation);

            if (angleDifference > angleThreshold)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, zoomInRotation, rotateTime * Time.deltaTime);
            }
            else
            {
                transform.rotation = zoomInRotation;
            }
        }

        float moveDifference = Vector3.Distance(transform.position, moveTo);

        if (moveDifference > moveThreshold)
        {
            transform.position = Vector3.Slerp(transform.position, moveTo, moveTime * Time.deltaTime);
        }
        else
        {
            transform.position = moveTo;
        }
    }

    //public void OnLeaveChair()
    //{
    //    Debug.Log("Leave Chair");
    //    switchCamera.Invoke();
    //}

    //public void OnTopDown()
    //{
    //    Debug.Log("Top Down");
    //    topDown.Invoke();
    //}

    public void RotateLeft()
    {
        if (lookingLeft || zoomedIn) return;

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
        if (lookingRight || zoomedIn) return;

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

    public void RotateStraight()
    {
        if (rotateTo == defaultRotation) return;

        rotateTo = defaultRotation;
        Debug.Log("Rotate straight");
    }

    public void ZoomIn(Vector3 movePosition, Quaternion moveRotation)
    {
        moveTo = movePosition;
        zoomInRotation = moveRotation;
        zoomedIn = true;
        Debug.Log("Zoomed in");
    }

    public void ZoomOut()
    {
        moveTo = defaultPosition;
        zoomedIn = false;
        Debug.Log("Zoomed out");
    }
}
