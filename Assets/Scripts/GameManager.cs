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
    }

    void Update()
    {
        
    }

    public void AddFalls()
    {
        falls++;
        if(falls < 3)
        {
            //screm pantalla
        }
        else if(falls == 3)
        {
            //game over
        }
    }
}
