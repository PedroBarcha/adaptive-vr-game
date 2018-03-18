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

public static class GlobalVariables {
    public static string category;

    public static bool startedPlaying = false;

    public static int correctGuesses = 0;

    public static CountryObject correctCountry;

    public static bool weather = false;

    public static Hashtable dimmingValues = new Hashtable()
    {
        {"Europe", .2f},
        {"Africa", .2f},
        {"America", .2f},
        {"Asia", .2f},
        {"Oceania", .2f}
    };

    public static List<string> photosphereCountries = new List<string>()
    {
        "USA",
        "Italy",
        "France",
        "England"
    };
    public static bool resume = false;

    //(id,country,continent,language,weather,x,y)
    //Weather: None = 0, Rain = 1, Snow = 2
    public static List<CountryObject> countryList = new List<CountryObject>()
    {
        new CountryObject(1, "Algeria", "Africa", "Arabic", 0, -22.92f, 51), 
        new CountryObject(2, "Australia", "Oceania", "English", 0, -0.3f, 294f), 
        new CountryObject(3, "Brazil", "America", "Portuguese", 0, 7.08f, 4.25f),
        new CountryObject(4, "Canada", "America", "French and English", 2, -21.36f, 348.5f), 
        new CountryObject(5, "China", "Asia", "Chinese", 0, 5.16f, -132.25f),
        new CountryObject(6, "Egypt", "Africa", "Arabic", 0, -21.84f, 71.25f), 
        new CountryObject(7, "France", "Europe", "French", 1, 8.16f, 126.5f), 
        new CountryObject(8, "Germany", "Europe", "German", 1, 3.48f, 134.25f), 
        new CountryObject(9, "Italy", "Europe", "Italian", 0, 13.92f, 137f), 
        new CountryObject(10, "England", "Europe", "English", 1, -1.08f, 122.75f), 
        new CountryObject(11, "Portugal", "Europe", "Portuguese", 0, 16.8f, 115.25f), 
        new CountryObject(12, "Russia", "Asia", "Russian", 2, -15.12f, -142.25f),
        new CountryObject(13, "Spain", "Europe", "Spanish", 0, 17.4f, 119.75f), 
        new CountryObject(14, "USA", "America", "English", 0, -14, 346.75f) 
    };

    public static CountryObject getRandomNonChosen()
    {
        int count = countryList.Count;
        List<int> nonChosen = new List<int>();
        for (int i = 0; i < count; i++)
        {
            if (countryList[i].chosen == false)
                nonChosen.Add(countryList[i].id);
        }
        int id = nonChosen[Random.Range(0, nonChosen.Count)];
        return getCountry(id);
    }

    public static CountryObject getCountry(int id)
    {
        foreach (CountryObject c in countryList)
        {
            if (c.id == id)
            {
                return c;
            }
        }
        return null;
    }
}
