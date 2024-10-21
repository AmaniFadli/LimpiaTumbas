using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] private List<ClenableProp> _gravePartsInOrder;

    // Start is called before the first frame update
    void Start()
    {
        if (_gravePartsInOrder.Count == 0)
        {
            _gravePartsInOrder.AddRange(GetComponentsInChildren<ClenableProp>());
        }
    }
}