using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
            Debug.Log("aadadad");
        }
        else if(falls == 3)
        {
            //game over
        }
    }
}
