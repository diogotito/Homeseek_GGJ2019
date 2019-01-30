using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenHeart : MonoBehaviour
{
    public ParticleSystem hearts;

    void Start()
    {
        //Instantiate(hearts, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), hearts.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(EmitParticles());
    }

    IEnumerator EmitParticles()
    {
        Instantiate(hearts, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), hearts.transform.rotation);
        yield return new WaitForSeconds(7f);
    }
}
