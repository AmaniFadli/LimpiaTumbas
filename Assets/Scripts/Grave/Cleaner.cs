using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Material _material;

    [FormerlySerializedAs("_dirtMaskTexture")] [SerializeField]
    private Texture2D _dirtMaskTextureBase;

    [SerializeField] private Texture2D _dirtBrush;

    private Texture2D _dirtyMaskTexture;

    [SerializeField] private float _dirtAmountTotal = 0f;

    [FormerlySerializedAs("_dirtAmountActual")] [FormerlySerializedAs("_dirtamountActual")] [SerializeField]
    private float _dirtAmount;

    [SerializeField] private Text _percentageText;

    // Start is called before the first frame update
    void Awake()
    {
        _dirtyMaskTexture = new Texture2D(_dirtMaskTextureBase.width, _dirtMaskTextureBase.height);
        _dirtyMaskTexture.SetPixels(_dirtMaskTextureBase.GetPixels());
        _dirtyMaskTexture.Apply();
        _material.SetTexture("_DirtyMask", _dirtyMaskTexture);
        CalculatePixel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
            {
                Vector2 textureCoord = raycastHit.textureCoord;

                int PixelX = (int)(textureCoord.x * _dirtMaskTextureBase.width);
                int PixelY = (int)(textureCoord.y * _dirtMaskTextureBase.height);

                Vector2Int paintPixelPosition = new Vector2Int(PixelX, PixelY);
                Debug.Log("UV: " + textureCoord + "; Pixels: " + paintPixelPosition);

                int pixelXOffset = PixelX - (_dirtBrush.width / 2);
                int pixelYOffset = PixelY - (_dirtBrush.height / 2);

                for (int i = 0; i < _dirtBrush.width; i++)
                {
                    for (int j = 0; j < _dirtBrush.height; j++)
                    {
                        Color pixelDirt = _dirtBrush.GetPixel(i, j);
                        Color pixelDirtMask = _dirtyMaskTexture.GetPixel(pixelXOffset + i, pixelYOffset + j);

                        float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                        _dirtAmount -= removedAmount;
                        int percentage = Mathf.RoundToInt(_dirtAmount / _dirtAmountTotal * 100);
                        _percentageText.text = "" + percentage;

                        _dirtyMaskTexture.SetPixel(
                            pixelXOffset + i,
                            pixelYOffset + j,
                            new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                    }
                }

                _dirtyMaskTexture.Apply();
            }
        }

        //int pixelXOffset = PixelX - 
    }

    void CalculatePixel()
    {
        for (int i = 0; i < _dirtMaskTextureBase.width; i++)
        {
            for (int j = 0; j < _dirtMaskTextureBase.height; j++)
            {
                _dirtAmountTotal += _dirtMaskTextureBase.GetPixel(i, j).g;
            }
        }

        _dirtAmount = _dirtAmountTotal;
        _percentageText.text = "100";
    }
}