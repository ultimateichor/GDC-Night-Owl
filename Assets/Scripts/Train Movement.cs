using UnityEngine;
using UnityEngine.UIElements;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private GameObject console;
    [SerializeField] private float maxMoveSpeed = 3f;
    [SerializeField] private float maxFuelDownTimer = 10f;
    [SerializeField] private int maxFuelAmount = 10;

    private float fuelDownTimer;
    private int currentMoveSpeed;
    private int currentFuelAmount;

    private void Start()
    {
        if (console == null)
        {
            Debug.LogWarning("The console isn't assigned in the Train Movement Script.");
        }

        fuelDownTimer = 0f;
        currentMoveSpeed = Mathf.RoundToInt(0.5f * maxMoveSpeed);
        currentFuelAmount = maxFuelAmount;
    }

    private void Update()
    {
        fuelDownTimer += 1 * Time.deltaTime;

        if (Mathf.RoundToInt(fuelDownTimer) >= maxFuelDownTimer)
        {
            if (currentFuelAmount > 0)
            {
                currentFuelAmount--;
            }
            else
            {
                currentMoveSpeed = 0;
                Debug.Log("The train ran out of fuel");
            }
            fuelDownTimer = 0f;
            Debug.Log(currentFuelAmount);
        }
    }

    public void Restart()
    {
        fuelDownTimer = 0f;
        currentMoveSpeed = Mathf.RoundToInt(0.5f * maxMoveSpeed);
        currentFuelAmount = maxFuelAmount;
    }

    public int GetMoveSpeed()
    {
        return currentMoveSpeed;
    }

    public void IncreaseFuelLevel(int fuelIncrease)
    {
        if ((currentFuelAmount + fuelIncrease) > maxFuelAmount)
        {
            currentFuelAmount = maxFuelAmount;
            fuelDownTimer = 0f;
            Debug.Log("Reached the max amount of fuel");
        }
        else
        {
            currentFuelAmount += fuelIncrease;
            fuelDownTimer = 0f;
            Debug.Log("Added " + fuelIncrease + " to fuel amount. New amount is: " + currentFuelAmount);
        }
    }
}
