using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timeToDestroy = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }


    IEnumerator destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }
}
