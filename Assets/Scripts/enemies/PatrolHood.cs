using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using System.Collections;

public class PatrolHood : MonoBehaviour
{
    public List<Transform> pointsToPatrol;
    public NavMeshAgent agent;
    public GameObject whoToSeek;
    public float distanceToChase;


    public int currIndx;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currIndx = 0;
        whoToSeek = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(patrolState());
    }


    public IEnumerator patrolState()
    {
        while (!onRange())
        {
            if (Vector3.Distance(new Vector3(pointsToPatrol[currIndx].position.x, transform.position.y, pointsToPatrol[currIndx].position.z), transform.position) < 0.4f)
            {
                currIndx = (++currIndx) % pointsToPatrol.Count;
            }

            agent.SetDestination(pointsToPatrol[currIndx].position);
            yield return null;
        }

        StartCoroutine(chase());
    }


    public IEnumerator chase()
    {
        while (onRange())
        {
            agent.SetDestination(whoToSeek.transform.position);
            yield return null;
        }

        StartCoroutine(patrolState());
    }


    public bool onRange()
    {
        return Vector3.Distance(transform.position, whoToSeek.transform.position) < distanceToChase;
    }
}

