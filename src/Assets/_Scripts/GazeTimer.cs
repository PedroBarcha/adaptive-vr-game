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
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GazeTimer : MonoBehaviour
{
    public static bool moveModule = false;
    public static bool started = false;
    public static bool lookAtHint = false;

    public static string activeContinent;
    public static string correctContinent;

    public static float gazeTimer = 1f;

    public static CountryObject correctCountry;
    public static CountryObject selectedCountry;

    private float wrongContGaze = 0f;
    private float elapsedTime = 0f;
    private float guessTimer = 0f;
    private float infoTimer = 0f;

    private float introTimer = 5f; //Are you Ready? Screen
    private int photosphereWarningTime = 2;

    //values that are fixed now, but will be adaptive, after data gathering and model training
    private int dimTime = 19;
    private float descriptionTimeFactor = 0.03f; 
    private int questionTimeOut = 53;

    private bool hint1 = false, hint2 = false, displayInfo = false, answeredCorrectly = false, photosphered = false, timeOut = false;

    private static AudioClip correctSound;

    public static string[] Continents = { "Africa", "America", "Asia", "Europe", "Oceania" };

    // Use this for initialization
    void Start()
    {
        correctSound = Resources.Load<AudioClip>("Sound/Correct");
        setText("", "Question:");
        setText("", "QuestionText");
        setText("", "CountdownTimer");
        GameObject.Find("HintImage").GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //camera update
        Transform cg1 = GameObject.Find("QuestionCanvasGroup").transform;
        cg1.transform.eulerAngles = new Vector3(90, 0, -Camera.main.transform.eulerAngles.y);

        //set the whole scene after coming from menu or from a photosphere
        if ((lookAtHint && !GlobalVariables.startedPlaying) || (lookAtHint && GlobalVariables.resume))
        {
            //photosphere
            if (GlobalVariables.resume)
            {
                setText("", "CountdownTimer");
                startGame(true);
            }

            //menu
            else
            {
                //hold on to the "Are You Ready?" message
                if (introTimer > 0)
                {
                    CountryPopulator.setUpFirstHint(GlobalVariables.category);
                    introTimer -= Time.deltaTime;
                    GameObject.Find("CountdownTimer").GetComponent<Text>().text = Mathf.CeilToInt(introTimer).ToString();
                }
                //set the first question
                else if (introTimer < 0)
                {
                    startGame();
                    setText("", "CountdownTimer");
                }
            }
        }

        //update variables
        correctCountry = GlobalVariables.correctCountry;
        guessTimer += Time.deltaTime;

        //if question of all the countries were already done, go back to menu
        if (GlobalVariables.correctGuesses >= GlobalVariables.countryList.Count)
        {
            GlobalVariables.startedPlaying = false;
            resetGame();
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }

        else //set everything related to te question
        {
            //adjust the song volume as the user approach the right answer
            if (correctCountry != null && !answeredCorrectly)
            {
                float score = correctCountry.score();
                float value = -1 * score;
                value += 235;
                value = (value * 0.95f) / 235.05f;
                value = Mathf.Min(1, Mathf.Max(0.05f, value));
                GameObject.Find("GameMusic").GetComponent<AudioSource>().volume = value;
            }

            //time to display some hints?
            if ((correctCountry != null && !string.IsNullOrEmpty(correctCountry.countryName)) && !answeredCorrectly)
            {
                elapsedTime += Time.deltaTime;
                correctContinent = correctCountry.continent;

                //hint 1: dim the light of the continents that don't contain the answer
                if (correctContinent != activeContinent)
                {
                    wrongContGaze += Time.deltaTime;
                    if (wrongContGaze > dimTime)
                    {
                        hint1 = true;
                        dimLights(0.001f, correctCountry.weather);
                    }
                }
            }

            //if user exceeds time limit (which will be an adptive value in the future), displays the correct answer
            if (correctCountry != null &&  guessTimer > (questionTimeOut) && !answeredCorrectly)
                timeOut = true;

            //when the user gets the right answer
            if ((selectedCountry != null && correctCountry.Equals(selectedCountry)) || timeOut || answeredCorrectly)
            {
                elapsedTime = 0f;

                if (!answeredCorrectly)
                {
                    GameObject.Find("GameMusic").GetComponent<AudioSource>().PlayOneShot(correctSound);
                    GameObject.Find("DownArrow").GetComponent<SpriteRenderer>().enabled = true; //arrow indicates where to look to get the description of the topic
                    writeToLog(correctCountry.countryName, guessTimer.ToString(), hint1.ToString(), hint2.ToString()); //TODO: ADD WEATHER HINT AND ADJUST DIM HINT (now it is always true)
                    answeredCorrectly = true;
                    displayInfo = true;
                    timeOut = false;
                }

                //update the Canvas that stays bellow the user
                if (displayInfo)
                {
                    infoTimer += Time.deltaTime;
                    string description = XmlParser.getInfo(GlobalVariables.category, correctCountry.countryName);
                    float descriptionTime = (5 + descriptionTimeFactor * (description.Length));

                    //shows the description of the asked topic, during a time relative to the number of chars in the description text.
                    //right now the descriptionTimeFactor is fixed, but it should be adaptive in the future
                    if (infoTimer < descriptionTime)
                    {
                        GameObject.Find("HintImage").GetComponent<Image>().enabled = false;
                        setText(description, "QuestionText");
                    }

                    //if the country has a photosphere, show a warning about it to the user and then take him to it
                    else
                    {
                        if (GlobalVariables.photosphereCountries.Contains(correctCountry.countryName))
                        {
                            Text questionText = GameObject.Find("QuestionText").GetComponent<Text>();
                            questionText.text = "\n\n\nLet's take a trip to " + correctCountry.countryName + "!!";

                            //warning done, let's travel
                            if (infoTimer > (descriptionTime + photosphereWarningTime))
                            {
                                SceneManager.LoadSceneAsync(correctCountry.countryName, LoadSceneMode.Single);
                                displayInfo = false;
                                photosphered = true;
                            }
                        }
                        else
                            displayInfo = false;
                    }
                }

                //after the description, reset the parameters and get a new question, if any
                if (!displayInfo)
                {
                    if (!photosphered)
                    {
                        GameObject.Find("GameMusic").GetComponent<AudioSource>().Stop();
                    }

                    infoTimer = 0;
                    wrongContGaze = 0;
                    guessTimer = 0f;

                    answeredCorrectly = false;
                    hint1 = false;
                    hint2 = false;

                    //mark the country as already seen in the global variables
                    GlobalVariables.correctGuesses++;
                    CountryObject temp = GlobalVariables.countryList.Find(c => c.Equals(correctCountry));
                    temp.chosen = true;

                    if (GlobalVariables.correctGuesses < GlobalVariables.countryList.Count)
                    {
                        GlobalVariables.correctCountry = GlobalVariables.getRandomNonChosen();
                        if (!photosphered)
                            CountryPopulator.setUpHint(GlobalVariables.correctCountry, GlobalVariables.category);
                    }
                }
            }
        }

    }
    //END OF UPDTADE()

    //write an external log of the statistics of the gameplay of the user
    public void writeToLog(string countryName, string guessTime, string hint1, string hint2)
    {
        IOOperations.WriteToSave("Country: " + countryName + "\n", true, false);
        IOOperations.WriteToSave("Correct Guess Time: " + guessTime + "\n");
        IOOperations.WriteToSave("Hint Level 1: " + hint1 + "\n");
        IOOperations.WriteToSave("Hint Level 2: " + hint2, false, true);
    }

    private void setText(string text, string name)
    {
        GameObject.Find(name).GetComponent<Text>().text = text;
    }

    //set the whole scene after coming from menu or from a photosphere
    private void startGame(bool resume = false)
    {
        if (!resume)
        {
            foreach (CountryObject c in CountryPopulator.countryList)
            {
                c.chosen = false;
            }
            GlobalVariables.correctGuesses = 0;
            GlobalVariables.startedPlaying = true;
            int index = Random.Range(1, CountryPopulator.countryList.Count + 1);
            GlobalVariables.correctCountry = GlobalVariables.getCountry(index);
            IOOperations.WriteToSave("Category:" + GlobalVariables.category, true, false, true);
            CountryPopulator.setUpHint(GlobalVariables.correctCountry, GlobalVariables.category);
        }
        else
        {
            CountryPopulator.setUpHint(GlobalVariables.correctCountry, GlobalVariables.category);
            GlobalVariables.resume = false;
            photosphered = false;
        }

        guessTimer = 0f;
    }

    //dim the lights of the continents that don't contain the right answer
    private void dimLights(float factor, int weather)
    {
        foreach (string continent in Continents)
        {
            Light light = GameObject.Find(continent + "Light").GetComponent<Light>();
            if (continent == correctContinent)
                continue;
            else if (light.intensity >= (float)GlobalVariables.dimmingValues[continent])
            {
                GameObject.Find(continent + "Light").GetComponent<Light>().intensity -= factor;
 //               if (weather != 0)
 //               {
                    Image OceaniaImage = GameObject.Find("OceaniaImage").GetComponent<Image>();
                    Image AsiaImage = GameObject.Find("AsiaImage").GetComponent<Image>();
                    Image AmericaImage = GameObject.Find("AmericaImage").GetComponent<Image>();
                    Image AfricaImage = GameObject.Find("AfricaImage").GetComponent<Image>();
                    Image EuropeImage = GameObject.Find("EuropeImage").GetComponent<Image>();
                    if (correctContinent != "Europe" && EuropeImage.color.r > 0.2)
                    {
                    EuropeImage.color = new Color(EuropeImage.color.r - 0.001f, EuropeImage.color.g - 0.001f, EuropeImage.color.b - 0.001f);
                    }
                    if (correctContinent != "Africa" && AfricaImage.color.r > 0.2)
                    {
                        AfricaImage.color = new Color(AfricaImage.color.r - 0.001f, AfricaImage.color.g - 0.001f, AfricaImage.color.b - 0.001f);
                    }
                    if (correctContinent != "Oceania" && OceaniaImage.color.r > 0.2)
                    {
                        OceaniaImage.color = new Color(OceaniaImage.color.r - 0.001f, OceaniaImage.color.g - 0.001f, OceaniaImage.color.b - 0.001f);
                    }
                    if (correctContinent != "Asia" && AsiaImage.color.r > 0.2)
                    {
                        AsiaImage.color = new Color(AsiaImage.color.r - 0.001f, AsiaImage.color.g - 0.001f, AsiaImage.color.b - 0.001f);
                    }
                    if (correctContinent != "America" && AmericaImage.color.r > 0.3)
                    {
                        AmericaImage.color = new Color(AmericaImage.color.r - 0.001f, AmericaImage.color.g - 0.001f, AmericaImage.color.b - 0.001f);
                    }
//                }
            }
        }
    }

    //when the questions are done, the game returns to the menu. Before it happens, this method must be used to reset the global parameters
    private void resetGame()
    {
        foreach (CountryObject c in CountryPopulator.countryList)
        {
            c.chosen = false;
        }
        GlobalVariables.correctGuesses = 0;
        GlobalVariables.correctCountry = null;

    }

}