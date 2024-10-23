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
        _dirtyMaskTexture = new Texture2D(_dirtyMaskTextureBase.width, _dirtyMaskTextureBase.height);
        _dirtyMaskTexture.SetPixels(_dirtyMaskTextureBase.GetPixels());
        _dirtyMaskTexture.Apply();
        _newMaterial.SetTexture("_DirtyMask", _dirtyMaskTexture);
        CalculatePixel();
    }

    public void cleanPixel(Texture2D brush, Vector2 textureCoord)
    {
        int PixelX = (int)(textureCoord.x * _dirtyMaskTexture.width);
        int PixelY = (int)(textureCoord.y * _dirtyMaskTexture.height);

        // Calcula el desplazamiento de píxeles
        int pixelXOffset = PixelX - (brush.width / 2);
        int pixelYOffset = PixelY - (brush.height / 2);

        // Aseguramos que los valores de pixelXOffset y pixelYOffset estén dentro de los límites
        pixelXOffset = Mathf.Clamp(pixelXOffset, 0, _dirtyMaskTexture.width - brush.width);
        pixelYOffset = Mathf.Clamp(pixelYOffset, 0, _dirtyMaskTexture.height - brush.height);

        // Ajustar el bloque de píxeles para no salir de los límites
        int blockWidth = Mathf.Min(brush.width, _dirtyMaskTexture.width - pixelXOffset);
        int blockHeight = Mathf.Min(brush.height, _dirtyMaskTexture.height - pixelYOffset);


        if (!_isClean && _dirtyMaskTexture)
        {
            bool alreadyCheckedOrder = false;

            // Obtenemos los píxeles del pincel y de la textura de la máscara
            Color[] dirtPixels = brush.GetPixels(0, 0, blockWidth, blockHeight);
            Color[] maskPixels = _dirtyMaskTexture.GetPixels(pixelXOffset, pixelYOffset, blockWidth, blockHeight);

            for (int i = 0; i < dirtPixels.Length; i++)
            {
                Color pixelDirt = dirtPixels[i];
                Color pixelDirtMask = maskPixels[i];

                float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                _dirtAmount -= removedAmount;

                // Actualizamos los píxeles de la máscara
                maskPixels[i] = new Color(0, pixelDirtMask.g * pixelDirt.g, 0);
            }

            _dirtyMaskTexture.SetPixels(pixelXOffset, pixelYOffset, blockWidth, blockHeight, maskPixels);

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

            percentage = Mathf.RoundToInt(_dirtAmount / _dirtAmountTotal * 100);

            if (!unaVez)
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

        _dirtyMaskTexture.Apply();
    }

    void CalculatePixel()
    {
        // Obtener todos los píxeles de la textura en un solo paso
        Color[] pixels = _dirtyMaskTextureBase.GetPixels();
        _dirtAmountTotal = 0f;

        // Recorrer el arreglo de píxeles y sumar el valor del canal verde
        
        for (int i = 0; i < pixels.Length; i+=16)
        {
            _dirtAmountTotal += pixels[i].g;
            _dirtAmountTotal += pixels[i+1].g;
            _dirtAmountTotal += pixels[i+2].g;
            _dirtAmountTotal += pixels[i+3].g;
            _dirtAmountTotal += pixels[i+4].g;
            _dirtAmountTotal += pixels[i+5].g;
            _dirtAmountTotal += pixels[i+6].g;
            _dirtAmountTotal += pixels[i+7].g;
            _dirtAmountTotal += pixels[i+8].g;
            _dirtAmountTotal += pixels[i+9].g;
            _dirtAmountTotal += pixels[i+10].g;
            _dirtAmountTotal += pixels[i+11].g;
            _dirtAmountTotal += pixels[i+12].g;
            _dirtAmountTotal += pixels[i+13].g;
            _dirtAmountTotal += pixels[i+14].g;
            _dirtAmountTotal += pixels[i+15].g;
        }
        //
        // for (int i = 0; i < _dirtyMaskTextureBase.width; i++)
        // {
        //     for (int j = 0; j < _dirtyMaskTextureBase.height; j++)
        //     {
        //         _dirtAmountTotal += _dirtyMaskTextureBase.GetPixel(i, j).g;
        //     }
        // }
        //
        // _dirtAmount = _dirtAmountTotal;
        // if (_percentageText != null)
        //     _percentageText.text = "100";
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