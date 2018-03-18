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
using UnityEngine.UI;

public class CountryPopulator : MonoBehaviour
{
    public static List<CountryObject> countryList = GlobalVariables.countryList;

    // Use this for initialization
    void Start()
    {
        if (string.IsNullOrEmpty(GlobalVariables.category))
            GlobalVariables.category = "Celebrity";
    }

    //set up the first hint, should be used right after coming from the menu
    public static void setUpFirstHint(string category)
    {
        Text questionText = GameObject.Find("QuestionText").GetComponent<Text>();
        questionText.text = Category.buildQuestion(category, true);
        Image im = GameObject.Find("HintImage").GetComponent<Image>();
        im.enabled = false;
    }

    //set up a question, given the country and the topic
    public static void setUpHint(CountryObject c, string category)
    {
        Text questionText = GameObject.Find("QuestionText").GetComponent<Text>();
        Image im = GameObject.Find("HintImage").GetComponent<Image>();
        im.enabled = true;
        questionText.text = Category.buildQuestion(category);
        Sprite sprite = Resources.Load<Sprite>(category + "/" + c.countryName);
        im.sprite = sprite;
        AudioClip countryMusic = Resources.Load<AudioClip>("Sound/Traditional/" + c.countryName);
        AudioSource m = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        m.clip = countryMusic;
        m.loop = true;
        m.Play();
        resetLights();
    }

    //toggle the canvas bellow the user, with the questions/informations/description
    public static void toggleCanvas(bool toShow, string canvasName, float x = 0, float y = 0, float z = 0)
    {
        GameObject canvasGroup = GameObject.Find(canvasName);
        CanvasGroup cg = canvasGroup.GetComponent<CanvasGroup>();
        if (!toShow)
        {
            cg.interactable = false;
            cg.alpha = 0;
            canvasGroup.transform.position = new Vector3(10000, 10000, 10000);
        }
        else
        {
            Vector3 originalPos = new Vector3(x, y, z);
            cg.interactable = true;
            cg.alpha = 1;
            canvasGroup.transform.position = originalPos;
        }
    }

    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    private static void resetLights()
    {
        foreach (string continent in GazeTimer.Continents)
        {
            GameObject.Find(continent + "Light").GetComponent<Light>().intensity = 1;
        }
        GameObject.Find("OceaniaImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
        GameObject.Find("AmericaImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
        GameObject.Find("AsiaImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
        GameObject.Find("AfricaImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
        GameObject.Find("EuropeImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

}
