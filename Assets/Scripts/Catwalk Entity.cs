using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CatwalkEntity : MonoBehaviour
{
    //every x seconds, roll a die
    //aggression level determines how hard it is to roll the die
    //get the length of time to survive then every so often increase the aggression level
    //if the die is below the aggression level, increase the movement of the entity
    //when the entity gets to max movement, wait a few seconds then kill the player.

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject movementLocation0;
    [SerializeField] private GameObject movementLocation1;
    [SerializeField] private GameObject movementLocation2;
    [SerializeField] private float secondsToAttack = 4.5f;
    [SerializeField] private int startingAgressionLevel = 2;
    [SerializeField] private int maxAggressionLevel = 10;
    [SerializeField] private int maxMovement = 2;
    [SerializeField] private float timeTillDeath = 3f;
    [SerializeField] private float aggressionIncreaseAmount = 5f;
    private int aggressionLevel;
    private int movementDie;
    private int movementLevel;
    private float attackTimer;
    private float deathTimer;
    private float increaseAggressionTimer;
    private float increaseAggressionSeconds;

    private void Start()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager isn't assigned in the Catwalk Entity script in GameObject " + gameObject.name);
        }

        if ((startingAgressionLevel < maxAggressionLevel) && startingAgressionLevel >= 0)
        {
            aggressionLevel = startingAgressionLevel;
        }
        else
        {
            aggressionLevel = 1;
            Debug.Log("Starting aggression level is higher than max aggression level, setting it to 1");
        }

        movementLevel = 0;
        attackTimer = 0f;
        increaseAggressionTimer = 0f;
        increaseAggressionSeconds = gameManager.GetSurvivalTime() / aggressionIncreaseAmount;
        deathTimer = 0f;
        MoveEntity();
    }

    private void Update()
    {
        attackTimer += 1 * Time.deltaTime;
        increaseAggressionTimer += 1 * Time.deltaTime;

        if (Mathf.Approximately(attackTimer, secondsToAttack) || (attackTimer > secondsToAttack))
        {
            int rolledDie = RollDie();

            if (rolledDie < aggressionLevel)
            {
                movementLevel++;
                MoveEntity();
                //Debug.Log("Increased movement level");
            }

            attackTimer = 0f;
        }

        if (Mathf.Approximately(increaseAggressionTimer, increaseAggressionSeconds) || (increaseAggressionTimer > increaseAggressionSeconds))
        {
            if (aggressionLevel < maxAggressionLevel)
            {
                aggressionLevel++;
                //Debug.Log("increased aggression level");
            }

            increaseAggressionTimer = 0f;
        }

        if (movementLevel == maxMovement)
        {
            deathTimer += 1 * Time.deltaTime;

            if (Mathf.Approximately(deathTimer, timeTillDeath) || (deathTimer > timeTillDeath))
            {
                KillPlayer();
            }
        }
        else
        {
            deathTimer = 0;
        }
    }

    private int RollDie()
    {
        movementDie = Random.Range(1, maxAggressionLevel);
        //Debug.Log("Rolled a " + movementDie);
        return movementDie;
    }

    public void TakeDamage()
    {
        movementLevel = 0;
        attackTimer = 0f;
        MoveEntity();
    }

    private void KillPlayer()
    {
        gameManager.GameOver();
    }

    public void Restart()
    {
        if ((startingAgressionLevel < maxAggressionLevel) && startingAgressionLevel >= 0)
        {
            aggressionLevel = startingAgressionLevel;
        }
        else
        {
            aggressionLevel = 1;
            Debug.Log("Starting aggression level is higher than max aggression level, setting it to 1");
        }

        movementLevel = 0;
        attackTimer = 0f;
        increaseAggressionTimer = 0f;
        increaseAggressionSeconds = gameManager.GetSurvivalTime() / 6;
        deathTimer = 0f;
        MoveEntity();
    }

    private void MoveEntity()
    {
        switch (movementLevel)
        {
            case 1:
                transform.position = movementLocation1.transform.position;
                break;
            case 2:
                transform.position = movementLocation2.transform.position;
                break;
            default:
                transform.position = movementLocation0.transform.position;
                break;
        }
    }
}
