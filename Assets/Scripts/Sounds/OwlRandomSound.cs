using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlRandomSound : MonoBehaviour
{
    private int frameCounter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        frameCounter++;
        if (frameCounter == 10)
        {
            frameCounter = 0;
            if (Random.Range(1,4) > 2)
            {
                GetComponent<StudioEventEmitter>().Play();
            }
        }
    }
}
