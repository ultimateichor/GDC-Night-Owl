using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeSurvivedText;

    private void Start()
    {
        if (timeSurvivedText == null)
        {
            timeSurvivedText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void EnableDeathScreen(int timeSurvived)
    {
        timeSurvivedText.text = "You Survived for: " + timeSurvived + " seconds.";
    }
}
