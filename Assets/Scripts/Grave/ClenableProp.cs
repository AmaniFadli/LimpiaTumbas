using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClenableProp : MonoBehaviour
{
    public Material Material;

    [SerializeField] private Texture2D _dirtMaskTextureBase;

    private Texture2D _dirtyMaskTexture;

    // Start is called before the first frame update
    void Awake()
    {
        Material.SetTexture("_DirtyMask", _dirtyMaskTexture);
    }

    // Update is called once per frame
    void Update()
    {
    }
}