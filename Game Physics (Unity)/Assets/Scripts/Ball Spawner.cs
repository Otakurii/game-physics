using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Assign your ball prefab here
    public Transform spawnPoint; // Assign the spawn point in the Inspector

    [HideInInspector] public GameObject currentBall;

    void Start()
    {
        SpawnBall(); // Spawn the first ball when the game starts
    }

    public void SpawnBall()
    {
        if (currentBall == null)
        {
            // Instantiate a new ball at the spawn point
            currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

            // Assign the spawner reference to the new ball
            BallScript ballScript = currentBall.GetComponent<BallScript>();
            if (ballScript != null)
            {
                ballScript.spawner = this;
            }
        }
    }

    public void ClearCurrentBall()
    {
        currentBall = null;
    }
}
