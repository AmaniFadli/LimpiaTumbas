using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject jumpscarePoint;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isStopped = true;
            other.transform.parent.GetComponent<GetJumpscared>().ActivateRotation(jumpscarePoint.transform);
        }
    }

    public void KillGhost()
    {
        //si hay tiempo poner particulas del unity basicas
        this.gameObject.SetActive(false);
    }
}
