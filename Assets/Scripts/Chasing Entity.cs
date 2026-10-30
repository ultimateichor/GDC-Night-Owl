using UnityEngine;

public class ChasingEntity : MonoBehaviour
{
    [SerializeField] private TrainMovement trainMovement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int maxDistanceFromPlayer = 10;
    [SerializeField] private int oneSpeedMovement = 0;
    [SerializeField] private int twoSpeedMovement = 1;
    [SerializeField] private int threeSpeedMovement = 2;
    [SerializeField] private int zeroSpeedMovement = 1;
    [SerializeField] private float timeTillDeath = 3f;
    [SerializeField] private float maxMoveTimer = 5f;

    private int currentTrainSpeed;
    private int distanceFromPlayer;
    private float moveTimer;
    private float deathTimer;

    private void Start()
    {
        if (trainMovement == null)
        {
            Debug.LogWarning("trainMovement is not assigned in the Chasing Entity script in gameObject " + gameObject.name);
        }
        if (gameManager == null)
        {
            Debug.LogWarning("gameManager is not assigned in the Chasing Entity script in gameObject " + gameObject.name);
        }

        distanceFromPlayer = maxDistanceFromPlayer;
        currentTrainSpeed = 0;
        deathTimer = 0;
        moveTimer = 0;
    }

    private void Update()
    {
        currentTrainSpeed = trainMovement.GetMoveSpeed();
        moveTimer += 1 * Time.deltaTime;

        if (Mathf.Approximately(moveTimer, maxMoveTimer) || moveTimer > maxMoveTimer)
        {
            switch (currentTrainSpeed)
            {
                case 1:
                    distanceFromPlayer += oneSpeedMovement;
                    break;
                case 2:
                    distanceFromPlayer += twoSpeedMovement;
                    break;
                case 3:
                    distanceFromPlayer += threeSpeedMovement;
                    break;
                default:
                    distanceFromPlayer += zeroSpeedMovement;
                    break;
            }

            if (distanceFromPlayer > maxDistanceFromPlayer)
            {
                distanceFromPlayer = maxDistanceFromPlayer;
            }
            else if (distanceFromPlayer < 0)
            {
                distanceFromPlayer = 0;
            }

            Debug.Log("The chaser is " + distanceFromPlayer + " away from the train");
            moveTimer = 0;
        }
        
        if (distanceFromPlayer <= 0)
        {
            deathTimer += 1 * Time.deltaTime;
            if (Mathf.Approximately(deathTimer, timeTillDeath) || deathTimer > timeTillDeath)
            {
                KillPlayer();
            }
        }
        else
        {
            deathTimer = 0;
        }
    }

    private void KillPlayer()
    {
        gameManager.GameOver();
    }
}
