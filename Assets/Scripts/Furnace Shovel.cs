using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class FurnaceShovel : MonoBehaviour
{
    private bool isPickedUp;
    private Vector3 startingPosition;

    private void Start()
    {
        isPickedUp = false;
        startingPosition = transform.position;
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
                    PickUp();
                }
            }
        }
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == this.transform)
                {
                    SetDown();
                }
            }
        }
    }

    private void PickUp()
    {
        //move the shovel from starting position to in the player's hand
        isPickedUp = true;
        Debug.Log("Picked up the shovel");
    }

    public void SetDown()
    {
        //put the shovel back to starting position
        isPickedUp = false;
        Debug.Log("Set down the shovel");
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
