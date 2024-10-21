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
            Vector3 shootDirection = spawnBullets.transform.forward;
            shootDirection.Normalize();
            float angleToPointer = Vector3.SignedAngle(Vector3.right, shootDirection, Vector3.forward);

            GameObject bulletObj = Instantiate(bulletPrefab, spawnBullets.position, Quaternion.identity);

            Quaternion localRotation = Quaternion.AngleAxis(-90, Vector3.forward);

            bulletObj.transform.localRotation = localRotation * bulletObj.transform.localRotation;

            localRotation = Quaternion.AngleAxis(angleToPointer, Vector3.forward);
            bulletObj.transform.localRotation = localRotation * bulletObj.transform.localRotation;

            bulletObj.GetComponent<WaterController>().Init(shootDirection);
            isShoot = true;
            ShootCoolDown = timeBetween;
        }
    }
}
