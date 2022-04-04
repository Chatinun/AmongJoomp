using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Light : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D pointLight;

    public static Light instance;

    void Start()
    {
        instance = this;
        globalLight.intensity = 1;
        pointLight.intensity = 0;
    }

    public void NightTime()
    {
        StartCoroutine(GoNightGlobal());
        StartCoroutine(GoNightPoint());
    }

    IEnumerator GoNightGlobal()
    {
        while(globalLight.intensity > 0.1f)
        {
            globalLight.intensity -= 0.001f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator GoNightPoint()
    {
        while (pointLight.intensity < 0.8f)
        {
            pointLight.intensity += 0.001f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
