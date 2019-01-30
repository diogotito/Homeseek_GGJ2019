using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MouseHover : MonoBehaviour
{
    public void MouseEnter()
    {
        Text self = GetComponent<Text>();
        self.color = new Color(239 / 255f, 128 / 255f, 44 / 255f);
    }

    public void MouseExit()
    {
        Text self = GetComponent<Text>();
        self.color = new Color(1, 1, 1);
    }
}
