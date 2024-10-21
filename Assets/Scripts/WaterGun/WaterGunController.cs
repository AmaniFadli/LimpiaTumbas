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

    [SerializeField] private PlayerInput inpuyt;

    void Start()
    {
        isShoot = false;
        inpuyt.onShootEvent.AddListener(ShootWater);
    }
    void Update()
    {
        ShootCoolDown -= Time.deltaTime;
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
}
