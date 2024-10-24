using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GetJumpscared : MonoBehaviour
{
    private Transform targetPoint; 
    [SerializeField] private float rotationSpeed = 5f;
    [FormerlySerializedAs("camera")] [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject cinemachineCam;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput input;
    [SerializeField] private PCamera cameraBlock;
    private bool canRotate = true;
    private FMOD.Studio.EventInstance screamSound;


    private bool isActivated = false;

    public void ActivateRotation(Transform targetPoint)
    {
        isActivated = true;
        this.targetPoint = targetPoint;
        BlockPlayerMovement();

    }

    public void DeactivateRotation()
    {
        isActivated = false;
    }

    void Update()
    {
        if (canRotate && isActivated && targetPoint != null)
        {
            StartCoroutine(RotateTowardsTarget());
        }
    }

    private IEnumerator RotateTowardsTarget()
    {
        canRotate = false; 
        float elapsedTime = 0f; 

        Quaternion initialRotation = _camera.transform.rotation; 
        Vector3 directionToTarget = targetPoint.position - transform.position;
        directionToTarget.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget); 


        float totalRotationTime = Quaternion.Angle(initialRotation, targetRotation) / rotationSpeed;
        screamSound = FMODUnity.RuntimeManager.CreateInstance("event:/Jumpscare");
        screamSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        screamSound.start();
        screamSound.release();
        while (elapsedTime < totalRotationTime)
        {

            _camera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / totalRotationTime);
            cinemachineCam.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / totalRotationTime);

            elapsedTime += Time.deltaTime; 
            yield return null; 
        }


        _camera.transform.rotation = targetRotation;
        cinemachineCam.transform.rotation = targetRotation;

        BlockPlayerCamera();
    
    }

    private void BlockPlayerMovement()
    {
        input.enabled = false;
    }

    private void BlockPlayerCamera()
    {
        cameraBlock.enabled = false;
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.GameOver();
    }
}  