using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    
    [SerializeField]private Material _material;

    private Texture2D _dirtMaskTexture;

    private Texture2D _dirtyMask;
    // Start is called before the first frame update
    void Start()
    {
        _dirtyMask = (Texture2D)_material.GetTexture("DirtyMask");
        _dirtMaskTexture = new Texture2D(_dirtyMask.width, _dirtyMask.height);
        _dirtMaskTexture.SetPixels(_dirtyMask.GetPixels());
        _dirtMaskTexture.Apply();
        _material.SetTexture("_DirtyMask", _dirtMaskTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
