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
    private bool isShooting;
    private FMOD.Studio.EventInstance waterSound;


    private bool isTaked;
    void Start()
    {
        LoadWaterSound();
        isTaked = false;
    }
    public void SetIsTaked(bool isTaked)
    {
        this.isTaked = isTaked;
    }
    void Update()
    {
        ShootWater();
    }
    public void ShootWater()
    {
        if(isTaked)
        {
            float shootInput = PlayerInput.instance.GetShootInput();
            if (shootInput == 1)
            {
                waterSound.setPaused(false);
                water.SetActive(true);
                if (Physics.Raycast(spawnRay.position, spawnRay.forward, out RaycastHit raycastHit, raycastDistance))
                {
                    Vector2 textureCoord = raycastHit.textureCoord;

                    if (raycastHit.collider.TryGetComponent<ClenableProp>(out ClenableProp clenableProp))
                    {
                        clenableProp.cleanPixel(_dirtBrush, textureCoord);
                    }
                }
            }
            else
            {
                waterSound.setPaused(true);
                water.SetActive(false);
            }
        } 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward * raycastDistance);
    }

    private IEnumerator RestartSound()
    {
        yield return new WaitForSeconds(52);
        LoadWaterSound();
        StartCoroutine(RestartSound());
    }
    private void LoadWaterSound()
    {
        waterSound = FMODUnity.RuntimeManager.CreateInstance("event:/WaterGun");
        waterSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        waterSound.start();
        waterSound.setPaused(true);
        StartCoroutine(RestartSound());
    }
}
