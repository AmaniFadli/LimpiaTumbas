using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Time to Shoot")]
    float ShootCoolDown = 0;
    [SerializeField] float timeBetween;
    private bool isShoot;

    [Header("Transforms")]
    [SerializeField] private Transform spawnBullets;

    private FMOD.Studio.EventInstance waterSound;
    private bool isShooting;
    [SerializeField] private PlayerInput inpuyt;

    private 

    void Start()
    {
        PlayWaterSound();
        isShoot = false;
        inpuyt.onShootEvent.AddListener(ShootWater);
    }
    void Update()
    {
        ShootCoolDown -= Time.deltaTime;
        if (isShooting )
        {
            waterSound.setPaused(false);
        }
        else
        {
            waterSound.setPaused(false);
        }
    }
    public void ShootWater()
    {
        float shootInput = PlayerInput.instance.GetShootInput();
        if(shootInput != 0 && !isShoot)
        {
            if(ShootCoolDown <= 0)
            {
                Vector3 shootDirection = spawnBullets.transform.forward;
                shootDirection.Normalize();
             
                GameObject bulletObj = Instantiate(bulletPrefab, spawnBullets.position, Quaternion.identity);
                Quaternion localRotation = Quaternion.Euler(90,0,0);
                bulletObj.transform.rotation = localRotation;
                bulletObj.GetComponent<WaterController>().Init(shootDirection);
                isShoot = true;
                ShootCoolDown = timeBetween;
            } 
        }
        else
        {
            isShoot = false;
        }
    }

    private void PlayWaterSound()
    {
        waterSound = FMODUnity.RuntimeManager.CreateInstance("event:/WaterGun");
        waterSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        waterSound.start();

    }

    public void StopWaterSound()
    {
        waterSound.setPaused(true);
        waterSound.release();
    }
}
