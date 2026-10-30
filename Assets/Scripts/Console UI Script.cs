using TMPro;
using UnityEngine;

public class ConsoleUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TrainMovement trainMovement;

    private int oldFuelLevel = -1;
    private int oldSpeedLevel = -1;

    private void Start()
    {
        if (fuelText == null)
        {
            Debug.LogWarning("fuelText isn't assigned in the Console UI Script in GameObject " + gameObject.name);
        }
        if (speedText == null)
        {
            Debug.LogWarning("speedText isn't assigned in the Console UI Script in GameObject " + gameObject.name);
        }
        if (trainMovement == null)
        {
            Debug.LogWarning("trainMovement isn't assigned in the Console UI Script in GameObject " + gameObject.name);
        }
    }

    private void Update()
    {
        if (oldFuelLevel != trainMovement.GetFuelLevel())
        {
            ChangeFuelText();
        }
        if (oldSpeedLevel != trainMovement.GetFuelLevel())
        {
            ChangeSpeedText();
        }
    }

    private void ChangeFuelText()
    {
        fuelText.text = "Fuel Level: " + trainMovement.GetFuelLevel();
    }

    private void ChangeSpeedText()
    {
        speedText.text = "Speed: " + trainMovement.GetMoveSpeed();
    }
}
