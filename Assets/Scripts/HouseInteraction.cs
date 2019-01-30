using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseInteraction : MonoBehaviour
{
    public static int wrongInteractions = 0;
    
    public BoxCollider trigger;
    public GameObject grumpyWomanPrefab;
    public Transform spawnPoint;
    public Transform highlightArea;
    public AiManager aiManager;

    [SerializeField] bool isCatInside = false;
    public GameObject grumpyWoman = null;

    private void Start()
    {
        wrongInteractions = 0;
        if (highlightArea != null) highlightArea.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCatInside = true;
            if (highlightArea != null) highlightArea.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCatInside = false;
        if (highlightArea != null) highlightArea.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isCatInside && grumpyWoman == null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/PLAYER/KNOCK_KNOCK", GetComponent<Transform>().position);
            
            // Check if this is the wrong home
            FinalHome finalHomeScript = GetComponent<FinalHome>();

            spawnWoman();
            if (finalHomeScript == null)
            {
                wrongInteractions++; 
                if (wrongInteractions == 2)
                {
                    StartCoroutine(WaitTime());
                    wrongInteractions = 0;
                }
            }
            else
            {
                grumpyWoman.GetComponent<GrumpyWoman>().isGrumpy = false;
                aiManager.destroyAgents();
                finalHomeScript.TriggerEnding();
            }
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1f);
        MovePlayer.gameOver();
    }

    public GrumpyWoman spawnWoman()
    {
        grumpyWoman = Instantiate(grumpyWomanPrefab, spawnPoint.position, spawnPoint.rotation);
        return grumpyWoman.GetComponent<GrumpyWoman>();
    }

}
