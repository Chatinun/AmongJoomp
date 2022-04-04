using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    public static ShakeScreen instance;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
