using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockKnockTest : MonoBehaviour
{
    private bool catInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            catInside = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            catInside = false;
        }
    }

    private void Update()
    {
        if (catInside && Input.GetButtonDown("Jump"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/PLAYER/KNOCK_KNOCK", GetComponent<Transform>().position);
        }
    }
}
