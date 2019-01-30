using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBuilding : MonoBehaviour
{
    // Update is called once per frame
    void Update () {
        Renderer renderer = GetComponentInChildren<Renderer> ();
        Material mat = renderer.material;
 
        float emission = Mathf.PingPong (Time.time, 0.25f);
        Color baseColor = new Color(0.2f, 0.15f, 0.05f, 0.1f);
 
        Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
 
        mat.SetColor ("_EmissionColor", finalColor);
    }
}
