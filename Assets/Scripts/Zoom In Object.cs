using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ZoomInObject : MonoBehaviour
{
    [SerializeField] private GameObject zoomObject;

    private bool isZoomedIn;
    public UnityEvent<Vector3, Quaternion> Zoom;
    public UnityEvent UnZoom;

    private void Start()
    {
        if (zoomObject == null)
        {
            zoomObject = GetComponentInChildren<GameObject>();
        }

        isZoomedIn = false;
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == this.transform)
                {
                    ZoomIn();
                }
            }
        }
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        if (isZoomedIn) return;
        Debug.Log("Player clicked on zoom object");

        Zoom.Invoke(zoomObject.transform.position, zoomObject.transform.rotation);
        isZoomedIn = true;
    }

    private void ZoomOut()
    {
        if (!isZoomedIn) return;
        Debug.Log("Player pressed the button to unzoom");

        UnZoom.Invoke();
        isZoomedIn = false;
    }
}
