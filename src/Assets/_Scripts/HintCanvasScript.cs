using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintCanvasScript : MonoBehaviour
{
    public void PointerEnter()
    {
        GameObject.Find("DownArrow").GetComponent<SpriteRenderer>().enabled = false;
        //GameObject.Find("HintCanvasGroup").GetComponent<CanvasGroup>().alpha = 0;
        GazeTimer.lookAtHint = true;
    }

    public void PointerExit()
    {
        GazeTimer.lookAtHint = false;
    }
}
