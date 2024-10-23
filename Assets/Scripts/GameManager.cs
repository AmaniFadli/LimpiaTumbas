using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject enemySpawner;
    private int falls;

    void Start()
    {
        falls = 0;
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void AddFalls()
    {
        falls++;
        if(falls < 3)
        {
            CameraShake.instance.onShake();
            enemySpawner.GetComponent<EnemySpawner>().SpawnEnemyOnMistake();
            Debug.Log("aadadad");
        }
        else if(falls == 3)
        {
            //game over
        }
    }
}
