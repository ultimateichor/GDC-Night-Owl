using UnityEngine;
using UnityEngine.UIElements;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private GameObject console;
    [SerializeField] private int maxMoveSpeed = 3;
    [SerializeField] private int maxFuelAmount = 10;
    [SerializeField] private float fastFuelDownSpeed = 5f;
    [SerializeField] private float mediumFuelDownSpeed = 10f;
    [SerializeField] private float slowFuelDownSpeed = 15f;

    private float fuelDownTimer;
    private int currentMoveSpeed;
    private int currentFuelAmount;
    private float currentFuelDownSpeed;

    private void Start()
    {
        if (console == null)
        {
            Debug.LogWarning("The console isn't assigned in the Train Movement Script.");
        }

        fuelDownTimer = 0f;
        currentMoveSpeed = 2;
        currentFuelAmount = maxFuelAmount;
    }

    private void Update()
    {
        fuelDownTimer += 1 * Time.deltaTime;

        switch (currentMoveSpeed)
        {
            case 1:
                currentFuelDownSpeed = slowFuelDownSpeed;
                break;
            case 2:
                currentFuelDownSpeed = mediumFuelDownSpeed;
                break;
            case 3:
                currentFuelDownSpeed = fastFuelDownSpeed;
                break;
            default:
                currentFuelDownSpeed = 0f;
                break;
        }

        if (Mathf.RoundToInt(fuelDownTimer) >= currentFuelDownSpeed && currentFuelDownSpeed != 0)
        {
            if (currentFuelAmount > 0)
            {
                currentFuelAmount--;
            }
            else
            {
                currentMoveSpeed = 0;
                //Debug.Log("The train ran out of fuel");
            }
            fuelDownTimer = 0f;
            //Debug.Log(currentFuelAmount);
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

    public int GetFuelLevel()
    {
        return currentFuelAmount;
    }

    public void IncreaseFuelLevel(int fuelIncrease)
    {
        if ((currentFuelAmount + fuelIncrease) > maxFuelAmount)
        {
            currentFuelAmount = maxFuelAmount;
            fuelDownTimer = 0f;
            //Debug.Log("Reached the max amount of fuel");
        }
        else
        {
            currentFuelAmount += fuelIncrease;
            fuelDownTimer = 0f;
            //Debug.Log("Added " + fuelIncrease + " to fuel amount. New amount is: " + currentFuelAmount);
        }
    }

    public void IncreaseSpeed()
    {
        if (currentMoveSpeed < maxMoveSpeed)
        {
            currentMoveSpeed++;
            //Debug.Log("Increased speed to " + currentMoveSpeed);
        }
        else
        {
            currentMoveSpeed = maxMoveSpeed;
        }
    }

    public void DecreaseSpeed()
    {
        if (currentMoveSpeed > 0)
        {
            currentMoveSpeed--;
            //Debug.Log("Decreased speed to " + currentMoveSpeed);
        }
        else
        {
            currentMoveSpeed = 0;
        }
    }
}
