using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FinalCutsceneAnimation))]
public class EndingButton : Editor
{
    public void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        FinalCutsceneAnimation myScript = (FinalCutsceneAnimation) target;
        if(GUILayout.Button("Trigger ending cutscene"))
        {
            FinalHome.niceWoman.spawnWoman().isGrumpy = false;
            myScript.StartCutscene();
        }
    }
}