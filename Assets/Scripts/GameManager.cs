using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject enemySpawner;
    private int fails;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private GameObject _initialNote;
    private bool _isFirstLoop = true;

    void Start()
    {
        _initialNote.SetActive(true);
        fails = 0;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddFails()
    {
        fails++;
        if (fails < 3)
        {
            CameraShake.instance.onShake();
            enemySpawner.GetComponent<EnemySpawner>().SpawnEnemyOnMistake();
            Debug.Log("aadadad");
        }
        else if (fails == 3)
        {
            GameOver();
        }
    }

    private void Update()
    {
        Note();
    }

    public void Note()
    {
        bool tab = PlayerInput.instance.GetTabInput();
        if (tab)
        {
            if (_isFirstLoop)
            {
                _initialNote.SetActive(false);
                _isFirstLoop = false;
            }

            noteCanvas.SetActive(true);
        }
        else
        {
            noteCanvas.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}