using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ClenableProp : MonoBehaviour
{
    public Material Material;

    [FormerlySerializedAs("_dirtMaskTextureBase")] [SerializeField] private Texture2D _dirtyMaskTextureBase;
    private Texture2D _dirtyMaskTexture;
    public Texture2D dirtyMaskTexture { get => _dirtyMaskTexture; private set => _dirtyMaskTexture = value; }
    public Texture2D dirtMaskTextureBase
    {
        get => _dirtyMaskTextureBase;
        private set => _dirtyMaskTextureBase = value;
    }
    public float dirtAmount
    {
        get => _dirtAmount;
        set => _dirtAmount = value;
    }

    public float dirtAmountTotal
    {
        get => _dirtAmountTotal;
        set => _dirtAmountTotal = value;
    }

    public Text percentageText
    {
        get => _percentageText;
        set => _percentageText = value;
    }

    [SerializeField] private float _dirtAmountTotal = 0f;
    [SerializeField] private float _dirtAmount;
    
    [SerializeField] private Text _percentageText;

    // Start is called before the first frame update
    void Awake()
    {
        dirtyMaskTexture = new Texture2D(_dirtyMaskTextureBase.width, _dirtyMaskTextureBase.height);
        dirtyMaskTexture.SetPixels(_dirtyMaskTextureBase.GetPixels());
        dirtyMaskTexture.Apply();
        Material.SetTexture("_DirtyMask", dirtyMaskTexture);
        CalculatePixel();
    }

    void CalculatePixel()
    {
        for (int i = 0; i < dirtMaskTextureBase.width; i++)
        {
            for (int j = 0; j < dirtMaskTextureBase.height; j++)
            {
                _dirtAmountTotal += dirtMaskTextureBase.GetPixel(i, j).g;
            }
        }

        _dirtAmount = _dirtAmountTotal;
        percentageText.text = "100";
    }
}