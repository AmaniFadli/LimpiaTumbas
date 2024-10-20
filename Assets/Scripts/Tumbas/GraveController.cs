using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveController : MonoBehaviour
{
    private const int NUMPARTS = 4;
    [SerializeField] private GameObject[] gravePartsDefault = new GameObject[NUMPARTS];
    private List<GameObject> newGraveParts = new List<GameObject>();

    private bool itsFall;
    private bool isInteracted;

    private void Awake()
    {
        isInteracted = false;
    }
    void Start()
    {
        itsFall = false;
    }
    public void SetIsInteracted(bool isInteracted)
    {
        this.isInteracted = isInteracted;
    }
    private void comproveOrder(GameObject part)
    {
        if(itsFall == false && isInteracted == true)
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

            if (j == k)
            {
                //iluminar la lapida
            }
            else
            {
                itsFall = true;
            }
        }
    }
}
