using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeSurvivedText;

    private void Start()
    {
        if (timeSurvivedText == null)
        {
            timeSurvivedText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void EnableWinScreen(int timeSurvived)
    {
        timeSurvivedText.text = "You Survived for: " + timeSurvived + " seconds.";
    }
}
