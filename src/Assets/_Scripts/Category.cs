using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category : MonoBehaviour
{

    private int id;

    public static string buildQuestion(string category, bool first = false)
    {
        string returnValue = "";
        if (first)
        {
            switch (category)
            {
                case "Food":
                    returnValue = "\n\n\n\nLet's get to know traditional food from around the world!\n\nAre you ready?";
                    break;
                case "Flag":
                    returnValue = "\n\n\n\nLet's get to know national flags from around the world!\n\nAre you ready?";
                    break;
                case "Landmark":
                    returnValue = "\n\n\n\nLet's get to know famous landmarks from around the world!\n\nAre you ready?";
                    break;
                case "Celebrity":
                    returnValue = "\n\n\n\nLet's get to know famous celebrities from around the world!\n\nAre you ready?";
                    break;
            }
        }
        else
        {
            switch (category)
            {
                case "Food":
                    returnValue = "Can you guess the origin of this food?";
                    break;
                case "Flag":
                    returnValue = "Can you guess to which country this flag belongs to?";
                    break;
                case "Landmark":
                    returnValue = "Can you guess where is this landmark from?";
                    break;
                case "Celebrity":
                    returnValue = "Can you guess the country of birth of this celebrity?";
                    break;
            }
        }
        return returnValue;
    }

    public static string getHint(string category)
    {
        string returnValue = "";
        switch (category)
        {
            case "Food":
                returnValue = "You can usually find this food in ";
                break;
            case "Flag":
                returnValue = "This flag belongs to a country in ";
                break;
            case "Landmark":
                returnValue = "You can find this landmark in a country in ";
                break;
            case "Celebrity":
                returnValue = "This celebrity's country is somewhere in ";
                break;
        }
        return returnValue;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
