using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject enemySpawner;
    private int falls;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject noteCanvas;
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
            gameOver.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void Update()
    {
        Note();
    }
    public void Note()
    {
        bool tab = PlayerInput.instance.GetTabInput();
        if(tab)
        {
            noteCanvas.SetActive(true);
        }
        else
        {
            noteCanvas.SetActive(false);
        }
    }
}
