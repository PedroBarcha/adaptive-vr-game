/****************************************************************************
/*   Copyright(C) <2018> <Pedro Barcha and Bachar Senno>
/*
/*    This program is free software: you can redistribute it and/or modify
/*    it under the terms of the GNU General Public License as published by
/*    the Free Software Foundation, either version 3 of the License, or
/*    (at your option) any later version.
/*
/*    This program is distributed in the hope that it will be useful,
/*    but WITHOUT ANY WARRANTY; without even the implied warranty of
/*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
/*    GNU General Public License for more details.
/*
/*    You should have received a copy of the GNU General Public License
/*    along with this program.If not, see<https://www.gnu.org/licenses/>. 
/**************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCategory : MonoBehaviour
{

    private bool gazedAt = false;
    private float timer = 0f;

    // Use this for initialization
    public void pickFood()
    {
        GlobalVariables.category = "Food";
        toggleOptions(false);
    }

    public void pickFlag()
    {
        GlobalVariables.category = "Flag";
        toggleOptions(false);
    }

    public void pickLandmark()
    {
        GlobalVariables.category = "Landmark";
        toggleOptions(false);
    }

    public void pickCelebrity()
    {
        GlobalVariables.category = "Celebrity";
        toggleOptions(false);
    }

    public void pickLanguage()
    {
        GlobalVariables.category = "Language";
        toggleOptions(false);
    }

    public static void toggleOptions(bool show)
    {
        if (!show)
        {
            GameObject.Find("OptionsCanvas").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("OptionsCanvas").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("MainMenuCanvas").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("MainMenuCanvas").GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            GameObject.Find("OptionsCanvas").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("OptionsCanvas").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("MainMenuCanvas").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("MainMenuCanvas").GetComponent<CanvasGroup>().alpha = 0;

        }
    }

    public void optionsButton()
    {
        toggleOptions(true);
    }

    private void Update()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 2.0f)
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

}
