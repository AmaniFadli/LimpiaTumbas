using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ClenableProp : MonoBehaviour
{
    #region variables

    [FormerlySerializedAs("Material")] public Material MaterialBase;
    private Material _newMaterial;


    [FormerlySerializedAs("_dirtMaskTextureBase")] [SerializeField]
    private Texture2D _dirtyMaskTextureBase;

    private Texture2D _dirtyMaskTexture;

    [SerializeField] private float _dirtAmountTotal = 0f;
    [SerializeField] private float _dirtAmount;

    [SerializeField] private Text _percentageText;
    private bool _isClean = false;
    private bool unaVez = false;
    private int percentage;

    public bool isClean
    {
        get => _isClean;
        set => _isClean = value;
    }

    [SerializeField] private float _cleanThreshold = 5f;

    private int _cleanPosition;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _newMaterial = new Material(MaterialBase);
        GetComponent<MeshRenderer>().material = _newMaterial;
        _dirtyMaskTexture = _dirtyMaskTextureBase;
        //_dirtyMaskTexture = new Texture2D(_dirtyMaskTextureBase.width, _dirtyMaskTextureBase.height);
        //_dirtyMaskTexture.SetPixels(_dirtyMaskTextureBase.GetPixels());
        //_dirtyMaskTexture.Apply();
        //_newMaterial.SetTexture("_DirtyMask", _dirtyMaskTexture);
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
        if (!_isClean && _dirtyMaskTextureBase)
        {
            bool alreadyCheckedOrder = false;
            int totalPixels = brush.width * brush.height;

            Color[] dirtPixels = brush.GetPixels();
            Color[] maskPixels = _dirtyMaskTextureBase.GetPixels(pixelXOffset, pixelYOffset, brush.width, brush.height);

            for (int i = 0; i < totalPixels; i++)
            {
                Color pixelDirt = dirtPixels[i];
                Color pixelDirtMask = maskPixels[i];

                float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                _dirtAmount -= removedAmount;

                // Actualizar la textura
                maskPixels[i] = new Color(0, pixelDirtMask.g * pixelDirt.g, 0);
            }

            _dirtyMaskTextureBase.SetPixels(pixelXOffset, pixelYOffset, brush.width, brush.height, maskPixels);

            /*
            for (int i = 0; i < brush.width; i++)
            {
                for (int j = 0; j < brush.height; j++)
                {
                    Color pixelDirt = brush.GetPixel(i, j);
                    Color pixelDirtMask =
                        _dirtyMaskTexture.GetPixel(pixelXOffset + i, pixelYOffset + j);

                    float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                    _dirtAmount -= removedAmount;

                    _dirtyMaskTexture.SetPixel(
                        pixelXOffset + i,
                        pixelYOffset + j,
                        new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                }
            }
*/

            percentage =
                Mathf.RoundToInt(_dirtAmount / _dirtAmountTotal * 100);

            //comprobamos si es la parte correcta
            if (unaVez == false)
            {
                GetComponentInParent<GraveController>().comproveOrder(this.gameObject);
                unaVez = true;
            }

            if (_percentageText != null)
                _percentageText.text = "" + percentage;
            if (percentage <= _cleanThreshold && !_isClean)
            {
                print("Clean all");
                CleanModel();
            }
        }

        _dirtyMaskTextureBase.Apply();
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
        if (_percentageText != null)
            _percentageText.text = "100";
    }

    void CleanModel()
    {
        // for (int i = 0; i < _dirtyMaskTexture.width; i++)
        // {
        //     for (int j = 0; j < _dirtyMaskTexture.width; j++)
        //     {
        //         _dirtyMaskTexture.SetPixel(i, j, Color.black);
        //     }
        // }
        int mipCount = _dirtyMaskTexture.mipmapCount;
        for (int i = 0; i < mipCount; i++)
        {
            int mipWidth = Mathf.Max(1, _dirtyMaskTexture.width >> i);
            int mipHeight = Mathf.Max(1, _dirtyMaskTexture.height >> i);

            // Crea un array de colores negros del tamaño correcto
            Color[] blackColors = new Color[mipWidth * mipHeight];

            // Rellena el array con colores negros
            for (int j = 0; j < blackColors.Length; j++)
            {
                blackColors[j] = Color.black;
            }

            // Establece los píxeles en el nivel actual de mipmap
            _dirtyMaskTexture.SetPixels(blackColors, i);
        }

        _dirtyMaskTexture.Apply();
        _isClean = true;
    }
}