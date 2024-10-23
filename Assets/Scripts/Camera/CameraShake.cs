using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void SetDuration(float duration)
    {
        this.duration = duration;
    }
    public void SetMagnitude(float magnitude)
    {
        this.magnitude = magnitude;
    }
    public void onShake()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(.1f);
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;

            float y = Random.Range(2.5f, 3f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
