using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using System.Collections;

public class KennelAgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject whoToSeek;

    private void Start()
    {
        whoToSeek = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        agent.SetDestination(whoToSeek.transform.position);

        if(!whoToSeek.transform.CompareTag("Player"))
        {
            if(Vector3.Distance(whoToSeek.transform.position, transform.position) < 0.4f)
                Destroy(this.gameObject);
        }
    }


    public IEnumerator Kill(float timeToKill)
    {
        yield return new WaitForSeconds(timeToKill);
       Destroy(this.gameObject);
    }
}
