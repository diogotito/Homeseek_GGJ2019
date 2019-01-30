using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class FinalCutsceneAnimation : MonoBehaviour
{
    public static FinalCutsceneAnimation instance;

    public Transform cat;
    public Camera gameCamera;
    public Camera cutsceneCamera;
    public Transform firstCatTransform;
    public Transform finalCatPosition;
    public Collider colliderToDisable;
    
    public bool triggered = false;

    private void Awake()
    {
        instance = this;
    }

    public void StartCutscene()
    {
        triggered = true;
        StartCoroutine(Cutscene());
    }

    private IEnumerator Cutscene()
    {
        //gameCamera.transform.SetParent(null, true);
        Vector3 gameCamInitialPos = gameCamera.transform.position;
        Quaternion gameCamInitialOrientation = gameCamera.transform.rotation;
        Vector3 catInitialPos = cat.position;
        Quaternion catInitialOrientation = cat.rotation;
        
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            gameCamera.transform.position = Vector3.Lerp(gameCamInitialPos, cutsceneCamera.transform.position, t);
            gameCamera.transform.rotation =
                Quaternion.Lerp(gameCamInitialOrientation, cutsceneCamera.transform.rotation, t);
            cat.position = Vector3.Lerp(catInitialPos, firstCatTransform.position, t);
            cat.GetComponentInChildren<MeshRenderer>().transform.rotation = Quaternion.Slerp(catInitialOrientation, firstCatTransform.rotation, t);
            yield return new WaitForEndOfFrame();
        }

        cutsceneCamera.enabled = true;
        gameCamera.enabled = false;
        yield return new WaitWhile(() => FinalHome.niceWoman.grumpyWoman != null);

        var catLegs = cat.GetComponent<CatAnimation>();
        colliderToDisable.enabled = false;

        for (float t = 0; t < 1; t += 0.5f * Time.deltaTime)
        {
            catLegs.Progress(Time.deltaTime * 4);
            cat.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(0.15f, 0.15f, 0.15f), (t - 0.5f) * 2);
            cat.position = Vector3.Lerp(firstCatTransform.position, finalCatPosition.position, t);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("EndScene");
    }
}
