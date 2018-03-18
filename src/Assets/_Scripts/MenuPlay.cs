using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPlay : MonoBehaviour
{

    private bool gazedAt = false;
    private float gazeTime = MenuGaze.gazeTimer;
    private float timer = 0f;

    // Use this for initialization
    void Start()
    {
        GlobalVariables.category = "Food";
    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
        }
        if (timer >= gazeTime)
        {
            ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            timer = 0f;
        }
    }

    public void PointerEnter()
    {
        gazedAt = true;
    }

    public void PointerExit()
    {
        gazedAt = false;
        timer = 0f;
    }

    public void PointerDown()
    {
        SceneManager.LoadScene("VR Map", LoadSceneMode.Single);
    }

}
