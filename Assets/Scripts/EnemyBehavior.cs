using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

    }


    void Update()
    {
        agent.SetDestination(player.transform.position);

        //cuando el enemigo haya sido spawneado con un prefab no funcionara porque no se le puede asignar la posicion del player. Se necesita hacer un singleton del player y hacer una funcion que devuelva las coordenadas del player
    }
}
