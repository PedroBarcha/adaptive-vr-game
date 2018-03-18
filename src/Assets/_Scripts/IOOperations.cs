using UnityEngine;
using System.IO;

public class IOOperations
{
    public static void WriteToSave(string data, bool separateS = false, bool separateF = false, bool clear = false)
    {
        string path = Application.persistentDataPath + "/save.txt";
        if (clear)
            File.WriteAllText(path, "");
        if (separateS)
            File.AppendAllText(path, ("\n=======================\n"));
        File.AppendAllText(path, data);
        if (separateF)
            File.AppendAllText(path, ("\n=======================\n"));
    }
}