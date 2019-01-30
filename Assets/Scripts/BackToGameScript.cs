using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGameScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(3f);
        if (Input.anyKeyDown)
            SceneManager.LoadScene("CitySceneTestAgents");
    }
}
