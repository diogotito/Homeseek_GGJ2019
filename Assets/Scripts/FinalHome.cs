using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalHome : MonoBehaviour
{
    public static HouseInteraction niceWoman;
    
    public bool endingTriggered = false;
    public FinalCutsceneAnimation cutsceneController;


    private void Start()
    {
        niceWoman = GetComponent<HouseInteraction>();
        print("HOUSE WITH NICE WOMAN = " + niceWoman);
    }

    public void TriggerEnding()
    {
        if (endingTriggered) return;
        endingTriggered = true;
        EndingTriggered();
    }

    private void EndingTriggered()
    {
        print("THAT'S ALL FOLKS!");
        cutsceneController.StartCutscene();
    }

    // Update is called once per frame
    
}
