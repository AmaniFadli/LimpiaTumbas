using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ClenableProp : MonoBehaviour
{
    #region variables

    public Material Material;

    [FormerlySerializedAs("_dirtMaskTextureBase")] [SerializeField]
    private Texture2D _dirtyMaskTextureBase;

    private Texture2D _dirtyMaskTexture;

    [SerializeField] private float _dirtAmountTotal = 0f;
    [SerializeField] private float _dirtAmount;

    [SerializeField] private Text _percentageText;
    private bool _isClean = false;

    public bool isClean
    {
        get => _isClean;
        set => _isClean = value;
    }

    [SerializeField] private float _cleanThreshold = 5f;

    private int _cleanPosition;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        _dirtyMaskTexture = new Texture2D(_dirtyMaskTextureBase.width, _dirtyMaskTextureBase.height);
        _dirtyMaskTexture.SetPixels(_dirtyMaskTextureBase.GetPixels());
        _dirtyMaskTexture.Apply();
        Material.SetTexture("_DirtyMask", _dirtyMaskTexture);
        CalculatePixel();
    }

    public void cleanPixel(Texture2D brush, Vector2 textureCoord)
    {
        int PixelX = (int)(textureCoord.x * _dirtyMaskTextureBase.width);
        int PixelY = (int)(textureCoord.y * _dirtyMaskTextureBase.height);

        //Vector2Int paintPixelPosition = new Vector2Int(PixelX, PixelY);
        //Debug.Log("UV: " + textureCoord + "; Pixels: " + paintPixelPosition);

        int pixelXOffset = PixelX - (brush.width / 2);
        int pixelYOffset = PixelY - (brush.height / 2);
        if (!_isClean)
        {
            for (int i = 0; i < brush.width; i++)
            {
                for (int j = 0; j < brush.height; j++)
                {
                    Color pixelDirt = brush.GetPixel(i, j);
                    Color pixelDirtMask =
                        _dirtyMaskTexture.GetPixel(pixelXOffset + i, pixelYOffset + j);

                    float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                    _dirtAmount -= removedAmount;
                    int percentage =
                        Mathf.RoundToInt(_dirtAmount / _dirtAmountTotal * 100);
                    _percentageText.text = "" + percentage;

                    _dirtyMaskTexture.SetPixel(
                        pixelXOffset + i,
                        pixelYOffset + j,
                        new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                    if (percentage <= _cleanThreshold && !_isClean)
                    {
                        print("Clean all");
                        CleanModel();
                    }
                }
            }
        }

        _dirtyMaskTexture.Apply();
    }

    void CalculatePixel()
    {
        for (int i = 0; i < _dirtyMaskTextureBase.width; i++)
        {
            for (int j = 0; j < _dirtyMaskTextureBase.height; j++)
            {
                _dirtAmountTotal += _dirtyMaskTextureBase.GetPixel(i, j).g;
            }
        }

        _dirtAmount = _dirtAmountTotal;
        _percentageText.text = "100";
    }

    void CleanModel()
    {
        for (int i = 0; i < _dirtyMaskTexture.width; i++)
        {
            for (int j = 0; j < _dirtyMaskTexture.width; j++)
            {
                _dirtyMaskTexture.SetPixel(i, j, Color.black);
            }
        }

        _isClean = true;
    }
}