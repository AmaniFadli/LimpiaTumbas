using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Texture2D _dirtBrush;

    // Start is called before the first frame update
    void Awake()
    {
        // _dirtyMaskTexture = new Texture2D(_dirtMaskTextureBase.width, _dirtMaskTextureBase.height);
        // _dirtyMaskTexture.SetPixels(_dirtMaskTextureBase.GetPixels());
        // _dirtyMaskTexture.Apply();
        // _material.SetTexture("_DirtyMask", _dirtyMaskTexture);
        //CalculatePixel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
            {
                Vector2 textureCoord = raycastHit.textureCoord;
                if (raycastHit.collider.TryGetComponent<ClenableProp>(out ClenableProp clenableProp))
                {
                    int PixelX = (int)(textureCoord.x * clenableProp.dirtMaskTextureBase.width);
                    int PixelY = (int)(textureCoord.y * clenableProp.dirtMaskTextureBase.height);

                    Vector2Int paintPixelPosition = new Vector2Int(PixelX, PixelY);
                    //Debug.Log("UV: " + textureCoord + "; Pixels: " + paintPixelPosition);

                    int pixelXOffset = PixelX - (_dirtBrush.width / 2);
                    int pixelYOffset = PixelY - (_dirtBrush.height / 2);

                    for (int i = 0; i < _dirtBrush.width; i++)
                    {
                        for (int j = 0; j < _dirtBrush.height; j++)
                        {
                            Color pixelDirt = _dirtBrush.GetPixel(i, j);
                            Color pixelDirtMask =
                                clenableProp.dirtyMaskTexture.GetPixel(pixelXOffset + i, pixelYOffset + j);

                            float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                            clenableProp.dirtAmount -= removedAmount;
                            int percentage =
                                Mathf.RoundToInt(clenableProp.dirtAmount / clenableProp.dirtAmountTotal * 100);
                            clenableProp.percentageText.text = "" + percentage;

                            clenableProp.dirtyMaskTexture.SetPixel(
                                pixelXOffset + i,
                                pixelYOffset + j,
                                new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                        }
                    }

                    clenableProp.dirtyMaskTexture.Apply();
                }
            }
        }
    }
}