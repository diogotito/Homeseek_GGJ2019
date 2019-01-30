using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AiManager : MonoBehaviour
{

    public List<GameObject> agents;
    public List<GameObject> agents2;

    public List<float> timetoKillInEachWave;
    public List<Transform> pointsToPatrol;

    public GameObject withdrawSpot1;
    public GameObject withdrawSpot2;
    public GameObject player;

    public GameObject agentChasePrefab;
    public GameObject agentPatrolPrefab;
    public GameObject spawnPatrolSpot;

    public int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


   public void destroyAgents()
    {
        for(int i = 0; i < agents.Count; i++)
        {
            if(agents[i] != null)
                Destroy(agents[i].gameObject);
        }

        for (int i = 0; i < agents2.Count; i++)
        {
            if (agents2[i] != null)
                Destroy(agents2[i].gameObject);
        }

    }


    public void spawnPatrolEnemy()
    {
        GameObject go = Instantiate(agentPatrolPrefab, spawnPatrolSpot.transform.position, agentPatrolPrefab.transform.rotation);
        go.GetComponent<PatrolHood>().pointsToPatrol = this.pointsToPatrol;
        agents2.Add(go);
    }


    public void spawnChaseEnemy(int numCluesFound)
    {

        float elapsed = 0.0f;

        if (numCluesFound == 3)
            spawnPatrolEnemy();

        if (agents.Count > 0)
        {
            return;
        }

        currentWave = Mathf.Min(numCluesFound-1,2);

        float minDistance = float.MaxValue;
        GameObject targetPosition = null;

        if (Vector3.Distance(player.transform.position, withdrawSpot1.transform.position) < minDistance)
        {
            targetPosition = withdrawSpot1;
            minDistance = Vector3.Distance(player.transform.position, withdrawSpot1.transform.position);
        }

        if (Vector3.Distance(player.transform.position, withdrawSpot2.transform.position) < minDistance)
        {
            targetPosition = withdrawSpot2;
            minDistance = Vector3.Distance(player.transform.position, withdrawSpot2.transform.position);
        }
        
        GameObject go = Instantiate(agentChasePrefab, targetPosition.transform.position, agentChasePrefab.transform.rotation);
        agents.Add(go);

        StartCoroutine(WithdrawingState());
    }


    IEnumerator WithdrawingState()
    {
        float elapsed = 0.0f;

        while (elapsed < timetoKillInEachWave[currentWave])
        {
            elapsed += Time.deltaTime;

            yield return null;
        }

        for (int i = 0; i < agents.Count; i++)
            withdrawAgent(agents[i]);

        for(int i = 0; i < agents.Count; i++)
            this.agents.Remove(agents[i]);
    }


    void withdrawAgent(GameObject agent)
    {
        float maxDistance = float.MinValue;
        GameObject targetPosition = null;

        if (agent == null)
            return;

        if( Vector3.Distance(agent.transform.position, withdrawSpot1.transform.position) > maxDistance )
        {
            targetPosition = withdrawSpot1;
            maxDistance = Vector3.Distance(agent.transform.position, withdrawSpot1.transform.position);
        }

        agent.GetComponent<KennelAgentController>().whoToSeek = targetPosition;
        agent.GetComponent<StudioEventEmitter>().Stop();
        Blink[] blinks = agent.GetComponentsInChildren<Blink>();

        for(int i = 0; i < blinks.Length; i++)
        {
            blinks[i].active = false;
        }

        StartCoroutine(agent.GetComponent<KennelAgentController>().Kill(40.0f));
    }
}
