using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float timeToTurnOn = 2;
    [SerializeField] float timeToTurnOff = 2;
    [SerializeField] float minIntensity = 0;
    [SerializeField] float maxIntensity = 2;
    public bool on;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = true;

        light = GetComponent<Light>();
        if(!on)
            StartCoroutine(turningOn());
        else
            StartCoroutine(turningOff());
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator turningOn()
    {
        on = true;
        float elapsed = 0.0f;

        while(elapsed < timeToTurnOn)
        {
            elapsed += Time.deltaTime;
            float intensity = Mathf.Lerp(minIntensity,maxIntensity,elapsed/timeToTurnOn);
            light.intensity = intensity;

            if (!active)
                light.intensity = 0.0f;

            yield return null;
        }

        StartCoroutine(turningOff());
    }


    IEnumerator turningOff()
    {
        on = false;
        float elapsed = 0.0f;

        while (elapsed < timeToTurnOn)
        {
            elapsed += Time.deltaTime;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, (1 - elapsed / timeToTurnOn));
            light.intensity = intensity;

            if (!active)
                light.intensity = 0.0f;

            yield return null;
        }

        StartCoroutine(turningOn());
    }
}
