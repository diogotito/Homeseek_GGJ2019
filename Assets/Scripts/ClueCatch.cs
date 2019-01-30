using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueCatch : MonoBehaviour
{
    public GameObject cluePanel;
    public Text clueText;
    public Image clueImage;
    public Image clueImage1;
    public Image clueImage2;
    public Image clueImage3;
    public Image clueImage4;
    public Sprite clueSprite;
    public Sprite clueSprite1;
    public Sprite clueSprite2;
    public Sprite clueSprite3;
    public Sprite clueSprite4;
    public GameObject clueVisual;
    public ParticleSystem clueSmell;
    public ParticleSystem clueAudio;
    public AiManager aiManager;

    public int numCluesFound;

    private bool hasClue = false;
    private bool hasClue1 = false;
    private bool hasClue2 = false;
    private bool hasClue3 = false;
    private bool hasClue4 = false;

    // Start is called before the first frame update
    void Start()
    {
        numCluesFound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StopAllCoroutines();
            clueVisual.SetActive(false);
            cluePanel.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("Clue"))
        {
            StopAllCoroutines();
            clueVisual.SetActive(false);
        }
        
        //visao
        if (col.gameObject.name == "Clue")
        {
            StartCoroutine(ShowClue("Where are they?"));
            clueImage.sprite = clueSprite;
            FMODUnity.RuntimeManager.PlayOneShot("event:/PLAYER/CLUE_SFX", GetComponent<Transform>().position);
            StartCoroutine(ShowClueImage());
            Destroy(col.gameObject);
            hasClue = true;
        }
        //paladar
        if (col.gameObject.name == "Clue (1)")
        {
            StartCoroutine(ShowClue("I miss the good food...especialy tuna"));
            clueImage1.sprite = clueSprite1;
            FMODUnity.RuntimeManager.PlayOneShot("event:/PLAYER/CLUE_SFX", GetComponent<Transform>().position);
            Destroy(col.gameObject);
            hasClue1 = true;
        }
        //audicao
        if (col.gameObject.name == "Clue (2)")
        {
            StartCoroutine(ShowClue("I feel so lonely but I hear them!"));
            clueImage2.sprite = clueSprite2;
            Instantiate(clueAudio, transform.position, clueAudio.transform.rotation);
            FMODUnity.RuntimeManager.PlayOneShot("event:/HOUSE_CLUES/HOUSE_TRUE_HINT", GetComponent<Transform>().position);
            Destroy(col.gameObject);
            hasClue2 = true;
        }
        //tacto
        if (col.gameObject.name == "Clue (3)")
        {
            StartCoroutine(ShowClue("I need cozy and dry place to sleep well next to my loving family."));
            clueImage3.sprite = clueSprite3;
            FMODUnity.RuntimeManager.PlayOneShot("event:/PLAYER/CLUE_SFX", GetComponent<Transform>().position);
            Destroy(col.gameObject);
            hasClue3 = true;
        }
        //olfacto
        if (col.gameObject.name == "Clue (4)")
        {
            StartCoroutine(ShowClue("Ohh...The smell of home."));
            clueImage4.sprite = clueSprite4;
            FMODUnity.RuntimeManager.PlayOneShot("event:/PLAYER/CLUE_SFX", GetComponent<Transform>().position);
            Instantiate(clueSmell, transform.position, clueSmell.transform.rotation);
            Destroy(col.gameObject);
            hasClue4 = true;
        }

        if (col.gameObject.CompareTag("Clue"))
        {
            numCluesFound++;
            aiManager.spawnChaseEnemy(numCluesFound);
        }

    }

    public void openPanelClue ()
    {
        if (hasClue)
        {
            StartCoroutine(ShowClue("Where are they?"));
            StartCoroutine(ShowClueImage());
        }
    }

    public void openPanelClue1()
    {
        if (hasClue1)
        {
            StartCoroutine(ShowClue("I miss the good food...especialy tuna"));
        }
    }

    public void openPanelClue2()
    {
        if (hasClue2)
        {
            StartCoroutine(ShowClue("I feel so lonely but I hear them!"));
            Instantiate(clueAudio, transform.position, clueAudio.transform.rotation);
            FMODUnity.RuntimeManager.PlayOneShot("event:/HOUSE_CLUES/HOUSE_TRUE_REHINT", GetComponent<Transform>().position);
        }
    }

    public void openPanelClue3()
    {
        if (hasClue3)
        {
            StartCoroutine(ShowClue("I need cozy and dry place to sleep well next to my loving family."));
        }
    }

    public void openPanelClue4()
    {
        if (hasClue4)
        {
            StartCoroutine(ShowClue("Ohh...The smell of home."));
            Instantiate(clueSmell, transform.position, clueSmell.transform.rotation);
        }
    }

    IEnumerator ShowClue(string str)
    {
        cluePanel.SetActive(true);
        clueText.text = str;
        yield return new WaitForSeconds(7f);
        cluePanel.SetActive(false);
    }

    IEnumerator ShowClueImage()
    {
        clueVisual.SetActive(true);
        yield return new WaitForSeconds(7f);
        clueVisual.SetActive(false);
    }
    
    
}
