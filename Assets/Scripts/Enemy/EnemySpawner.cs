using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject mistakeEnemy;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius; // Radio alrededor del jugador donde se puede spawnear
    [SerializeField] private float minSpawnDistance = 5f;  //Distancia a la que se quiere que el enemigo respecto al player
    [SerializeField] private float minminSpawnTime = 5f;
    [SerializeField] private float maxSpawnTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies(minminSpawnTime, maxSpawnTime)); // Inicia el coroutine al empezar
    }

    private IEnumerator SpawnEnemies(float minSpawnTime, float maxSpawnTime)
    {
        while (true)
        {
            float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomDelay);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool foundPosition = false;

        while (!foundPosition)
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += player.position;

            if (Vector3.Distance(randomDirection, player.position) >= minSpawnDistance) // Comprobar si el punto de spawn se encuentra dentro del radio y la distancia mínima
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

    public void SpawnEnemyOnMistake()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool foundPosition = false;

        while (!foundPosition)
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += player.position;

            if (Vector3.Distance(randomDirection, player.position) >= minSpawnDistance) // Comprobar si el punto de spawn se encuentra dentro del radio y la distancia mínima
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, NavMesh.AllAreas))
                {
                    spawnPosition = hit.position;
                    foundPosition = true;
                }
            }
        }

        Instantiate(mistakeEnemy, spawnPosition, Quaternion.identity);
    }
}
