using UnityEngine;
using UnityEngine.InputSystem;

public class FurnaceObject : MonoBehaviour
{
    [SerializeField] private FurnaceShovel shovel;
    [SerializeField] private TrainMovement trainManager;
    [SerializeField] private int fuelAmount = 2;

    private void Start()
    {
        if (shovel == null)
        {
            Debug.LogWarning("Shovel is not assigned in the Furnace Object Script found in the " + gameObject.name + " object.");
        }
        if (trainManager == null)
        {
            Debug.LogWarning("Train Movement is not assigned int the Furnace Object Script found in the " + gameObject.name + " object.");
        }
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == this.transform && shovel.IsPickedUp())
                {
                    AddToFuel();
                }
            }
        }
    }

    private void AddToFuel()
    {
        trainManager.IncreaseFuelLevel(fuelAmount);
    }
}
