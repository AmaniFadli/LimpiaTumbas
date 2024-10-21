using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField] private MovementBehaviour MB;
    private Vector3 direction;
    [SerializeField] private GameObject particleExposion;
    [SerializeField] private float timeToDestroy;
    private float time;
    void Awake()
    {
        MB = GetComponent<MovementBehaviour>();
    }
    public void Init(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        MB.Move(direction);

        time += Time.deltaTime;
        if (time >= timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sucio"))
        {
            Destroy(this.gameObject);
        }

    }
}
