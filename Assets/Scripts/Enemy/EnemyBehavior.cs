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
        agent.SetDestination(PlayerController.instance.GetPlayerPosition());
        
    }
}
