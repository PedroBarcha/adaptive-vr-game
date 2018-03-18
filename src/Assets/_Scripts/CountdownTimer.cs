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
using UnityEngine.SceneManagement;

//load photosphere scene
public class CountdownTimer : MonoBehaviour
{

    private float countdown = 23f;
    private bool flag = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !flag)
        {
            GameObject.Find("GameMusic").GetComponent<AudioSource>().Stop();
            GlobalVariables.resume = true;
			SceneManager.LoadSceneAsync("VR Map", LoadSceneMode.Single);
            flag = true;
        }
    }
}