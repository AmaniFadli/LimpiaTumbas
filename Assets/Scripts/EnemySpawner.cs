using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius; // Radio alrededor del jugador donde se puede spawnear
    [SerializeField] private float minSpawnDistance = 5f;  //Distancia a la que se quiere que el enemigo respecto al player

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool foundPosition = false;

        while (!foundPosition)
        {
        
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += player.position;

            if (Vector3.Distance(randomDirection, player.position) >= minSpawnDistance) //mirar si el punto de spawn se encuentra dentro del radio y el spawn distance
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, NavMesh.AllAreas))
                {
                    spawnPosition = hit.position;
                    foundPosition = true; 
                }
            }
        }

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnEnemy();
        }
    }
}
