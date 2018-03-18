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
using System.Xml;
using System.IO;
using UnityEngine.UI;


//given the topic and the country of the current question, returns a description to it.
//the info is retrieved from the xml file/database.
public static class XmlParser
{

    public static TextAsset xmlRawFile = Resources.Load<TextAsset>("DescriptionXml");
    public static string xmlData = xmlRawFile.text;

    
    public static string parseCelebrity(string country)
    {
        string finalText = "";
        string celebrity;
        string description;
        string xmlPathPattern = "//Topic/Celebrity/" + country;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        XmlNodeList nodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode countryNode in nodeList)
        {
            XmlNode celebrityNode = countryNode.FirstChild;
            celebrity = celebrityNode.Name;
            description = celebrityNode.InnerText;

            if (celebrityNode.Attributes != null)
            {
                if (celebrityNode.Attributes["surname"] != null)
                {
                    string surname = celebrityNode.Attributes["surname"].Value;
                    celebrity = celebrity + ' ' + surname;
                }

                if (celebrityNode.Attributes["time"] != null)
                {
                    string time = celebrityNode.Attributes["time"].Value;
                    celebrity = celebrity + ' ' + time;
                }
            }

            finalText = "\n\n\n\n<size=18><b>" + country + "</b>\n";
            finalText += "<b>Celebrity:</b>" + celebrity + "</size>\n";
            finalText += "<size=16>" + description + "</size>";
        }
        return finalText;
    }

    public static string parseFlag(string country)
    {
        string finalText = "";
        string description;
        string xmlPathPattern = "//Topic/Flag/" + country;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        XmlNodeList nodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode countryNode in nodeList)
        {
            description = countryNode.InnerText;

            finalText = "\n\n\n\n<size=18><b>" + country + "</b></size>\n";
            finalText += "<size=16>" + description + "</size>";
        }
        return finalText;
    }

    public static string parseFood(string country)
    {
        string finalText = "";
        string food;
        string description;
        string xmlPathPattern = "//Topic/Food/" + country;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        XmlNodeList nodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode countryNode in nodeList)
        {
            XmlNode foodNode = countryNode.FirstChild;
            food = foodNode.Name;
            description = foodNode.InnerText;

            if (foodNode.Attributes != null && foodNode.Attributes["surname"] != null)
            {
                string surname = foodNode.Attributes["surname"].Value;
                food = food + ' ' + surname;
            }

            finalText = "\n\n\n\n<size=18><b>" + country + "</b>\n";
            finalText += "<b>Food:</b> " + food + "</size>\n";
            finalText += "<size=16>" + description + "</size>";
        }
        return finalText;
    }

    public static string parseLandmark(string country)
    {
        string finalText = "";
        string landmark;
        string description;
        string location;
        string time;

        string xmlPathPattern = "//Topic/Landmark/" + country;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        XmlNodeList nodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode countryNode in nodeList)
        {
            XmlNode landmarkNode = countryNode.FirstChild;
            landmark = landmarkNode.Name;
            description = landmarkNode.InnerText;

            //it's not every landmark that has a surname
            if (landmarkNode.Attributes != null && (landmarkNode.Attributes["surname"] != null))
            {
                string surname = landmarkNode.Attributes["surname"].Value;
                landmark = landmark + ' ' + surname;
            }

            //but every landmark has time and city
            time = landmarkNode.Attributes["time"].Value;
            location = landmarkNode.Attributes["city"].Value;

            finalText = "\n\n\n\n\n<size=18><b>" + country + "</b>\n";
            finalText += "<b>Landmark:</b> " + landmark + '\n';
            finalText += "<b>Location:</b>" + location + '\n';
            finalText += "<b>Built in:</b> " + time + "\n</size>";
            finalText += "<size=16>" + description + "</size>";
        }
        return finalText;
    }

    public static string getInfo(string category, string country)
    {
        if (category == "Food")
            return parseFood(country);
        if (category == "Landmark")
            return parseLandmark(country);
        if (category == "Celebrity")
            return parseCelebrity(country);
        if (category == "Flag")
            return parseFlag(country);
        return "";
    }
}