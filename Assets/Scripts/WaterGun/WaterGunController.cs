using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject water;

    [Header("Textura")]
    [SerializeField] private Texture2D _dirtBrush;

    [Header("RayCast")]
    [SerializeField] private float raycastDistance;
    [SerializeField] private Transform spawnRay;

    void Start()
    {
        
    }
    void Update()
    {
        ShootWater();
    }
    public void ShootWater()
    {
        float shootInput = PlayerInput.instance.GetShootInput();
        if(shootInput == 1)
        {
            if (Physics.Raycast(spawnRay.position, spawnRay.forward, out RaycastHit raycastHit, raycastDistance))
            {
                Vector2 textureCoord = raycastHit.textureCoord;

                if (raycastHit.collider.TryGetComponent<ClenableProp>(out ClenableProp clenableProp))
                {
                    clenableProp.cleanPixel(_dirtBrush, textureCoord);
                    water.SetActive(true);
                }
                else
                {
                    water.SetActive(false);
                }
            }
            else
            {
                water.SetActive(false);
            }
        }
        else
        {
            water.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward * raycastDistance);
    }
}
