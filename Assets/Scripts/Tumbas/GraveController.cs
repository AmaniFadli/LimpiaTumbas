using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveController : MonoBehaviour
{
    [SerializeField] private const int NUMPARTS = 3;
    [SerializeField] private GameObject[] gravePartsDefault = new GameObject[NUMPARTS];
    private List<GameObject> newGraveParts = new List<GameObject>();
    private FMOD.Studio.EventInstance horrorSound;

    private bool itsFall;

    void Start()
    {
        itsFall = false;
    }
 
    public void comproveOrder(GameObject part)
    {
        if(itsFall == false)
        {
            newGraveParts.Add(part);
            int j = 0;
            int k = 0;
            for (int i = 0; i < gravePartsDefault.Length; i++)
            {
                if (gravePartsDefault[i] == part)
                {
                    j = i;
                }
            }
            for (int i = 0; i < newGraveParts.Count; i++)
            {
                if (newGraveParts[i] == part)
                {
                    k = i;
                }
            }

            if (j != k)
            {
                itsFall = true;
                Debug.Log("shake");
                horrorSound = FMODUnity.RuntimeManager.CreateInstance("event:/FailedPuzzle");
                horrorSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                horrorSound.start();
                horrorSound.release();
                GameManager.Instance.AddFalls();
            }
        }
    }
}
