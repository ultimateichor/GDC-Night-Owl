using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShotgunScript : MonoBehaviour
{

    private bool isPickedUp;
    private Vector3 startingPosition;
    [SerializeField] private List<GameObject> entitiesToHit;
    [SerializeField] private int maxAmmo = 5;

    private int currentAmmo;

    public UnityEvent shootEntity;

    private void Start()
    {
        isPickedUp = false;
        startingPosition = transform.position;
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit) && !isPickedUp)
            {
                if (hit.transform == this.transform)
                {
                    PickUp();
                }
            }
            else if (isPickedUp)
            {
                Shoot(ray);
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

    private void Shoot(Ray ray)
    {
        if (currentAmmo <= 0)
        {
            //Play a clicking sound to tell the player they are out of ammo
            Debug.Log("Out of ammo");
            return;
        }

        Debug.Log("Shot the shotgun");
        if (Physics.Raycast(ray, out RaycastHit hit) && isPickedUp)
        {
            foreach (GameObject entity in entitiesToHit)
            {
                if (hit.transform == entity.transform)
                {
                    shootEntity.Invoke();
                    Debug.Log("Hit the entity");
                }
                else
                {
                    Debug.Log("Missed the shot");
                }
            }
        }

        currentAmmo--;
        Debug.Log("Current ammo: " + currentAmmo);
    }

    private void PickUp()
    {
        //move the shotgun from starting position to in the player's hand
        isPickedUp = true;
        Debug.Log("Picked up the shotgun");
    }

    private void SetDown()
    {
        //put the shotgun back to starting position
        isPickedUp = false;
        Debug.Log("Set down the shotgun");
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
