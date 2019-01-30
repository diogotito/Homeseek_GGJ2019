using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InitialCutsceneAnimation : MonoBehaviour
{
    public Transform car;
    public Vector3 initialCarPosition;
    public Vector3 stoppingCarPosition;
    public Vector3 finalCarPosition;
    public Transform catRoot;
    
    public Camera cutsceneCamera;
    public Camera gameCamera;
    private Transform catModel;

    [SerializeField] private Vector3 startingCatPosition;
    [SerializeField] private bool skipCutscene = false;

    void Start()
    {
        startingCatPosition = catRoot.transform.position;
        catRoot.transform.position = car.transform.position;
        catModel = catRoot.GetComponentInChildren<MeshRenderer>().transform;
        StartCoroutine(AnimationCoroutine());
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            skipCutscene = true;
        }
    }
    
    private IEnumerator AnimationCoroutine()
    {
        var initialCatRotation = catRoot.transform.rotation;
        
        // Car starting from initialCarPosition and stopping at stoppingCarPosition
        catRoot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            car.position = Vector3.Lerp(initialCarPosition, stoppingCarPosition, t);
            catRoot.transform.position = car.transform.position;
            if (!skipCutscene) yield return new WaitForEndOfFrame();
        }

        if (!skipCutscene) yield return new WaitForSeconds(0.5f);

        // Cat exiting from car

        for (float t = 0; t < 1; t += 2 * Time.deltaTime)
        {
            catRoot.transform.position = Vector3.Lerp(car.transform.position, startingCatPosition, t);
            catRoot.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1, 1, 1), (t - 0.25f) * 4 / 3);
            if (!skipCutscene) yield return new WaitForEndOfFrame();
        }
        catRoot.transform.position = startingCatPosition;

        yield return new WaitForSeconds(0.5f);

        // Car abandoning cat

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            car.position = Vector3.Lerp(stoppingCarPosition, finalCarPosition, t);
            catModel.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 180, 0), t * 4);
            if (!skipCutscene) yield return new WaitForEndOfFrame();
        }

        if (!skipCutscene) yield return new WaitForSeconds(0.5f);

        // Transition to 3rd person camera

        Vector3 startingCamPos = cutsceneCamera.transform.position;
        Quaternion startingCamRotation = cutsceneCamera.transform.rotation;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            cutsceneCamera.transform.position = Vector3.Lerp(startingCamPos, gameCamera.transform.position, t);
            cutsceneCamera.transform.rotation = Quaternion.Slerp(startingCamRotation, gameCamera.transform.rotation, t);
            catModel.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, 180, 0), Quaternion.Euler(0, 0, 0), t * 2);
            if (!skipCutscene) yield return new WaitForEndOfFrame();
        }

        catRoot.transform.rotation = initialCatRotation;
        startGameplay();
    }

    private void startGameplay()
    {
        UnityEngine.Object.Destroy(car.gameObject);
        gameCamera.enabled = true;
        cutsceneCamera.enabled = false;
        catRoot.GetComponent<CharacterController>().enabled = true;
        catRoot.GetComponent<BoxCollider>().enabled = true;
        catRoot.GetComponent<MovePlayer>().enabled = true;
        catRoot.GetComponent<ClueCatch>().enabled = true;
    }

}
