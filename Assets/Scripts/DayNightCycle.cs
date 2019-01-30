using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Light light;
    [SerializeField] float timeToNight = 10;
    [SerializeField] float timeToDay = 10;
    [SerializeField] float shadowStrength = 0.413f;
    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(toNight());
    }


    IEnumerator toNight()
    {
        on = true;
        float elapsed = 0.0f;

        while (elapsed < timeToNight)
        {
            elapsed += Time.deltaTime;
            Color color = Color.Lerp(color1, color2, elapsed / timeToNight);
            float shadowStregth = Mathf.Lerp(shadowStrength, 0.0f, elapsed / timeToNight);
            light.shadowStrength = shadowStregth;
            light.color = color;
            yield return null;
        }

        StartCoroutine(toDay());
    }


    IEnumerator toDay()
    {
        on = false;
        float elapsed = 0.0f;

        while (elapsed < timeToDay)
        {
            elapsed += Time.deltaTime;
            Color color = Color.Lerp(color1, color2, (1 - elapsed / timeToDay));
            float shadowStregth = Mathf.Lerp(shadowStrength, 0.0f, (1 - elapsed / timeToDay));
            light.shadowStrength = shadowStregth;
            light.color = color;
            yield return null;
        }

        StartCoroutine(toNight());
    }
}
