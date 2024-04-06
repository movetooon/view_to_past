using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public static class DialogStorage  
{
    private static CharachterList charachterDialogList;
    private static MonologList monologList;

    public static void SetDialogsForCurrentLevel(string levelName)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/DialogsSource/" + levelName + ".json");
        charachterDialogList = JsonConvert.DeserializeObject<CharachterList>(jsonString);
    }

    public static void SetMonologsForCurrentLevel(string levelName)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/DialogsSource/" + levelName + "_monologs.json"); 
        monologList = JsonConvert.DeserializeObject<MonologList>(jsonString);
    }

    public static Monolog GetMonologByName(string monologName )
    {
        return monologList.monologs[monologName];
    }

    public static Dialog GetCharachterDialog(string charachterName, int dialogNumber)
    {  
        return charachterDialogList.charachters[charachterName].dialogs[dialogNumber]; 
    }

    public static bool TryGetCharachterDialog(string charachterName, int dialogNumber,out Dialog dialog)
    {
        dialog = new Dialog();
        try
        {
            dialog= charachterDialogList.charachters[charachterName].dialogs[dialogNumber];
            return true;
        }
        catch(Exception ex)
        { 
            return false;
        }
         
    }
}

#region dialog structs
 
[System.Serializable]
struct CharachterList
{
    public Dictionary<string, Charachter> charachters;
}
struct MonologList
{
    public Dictionary<string, Monolog> monologs;
}

[System.Serializable] 
struct Charachter
{ 
    public List<Dialog> dialogs;  
}


[System.Serializable]
public struct Dialog
{ 
    public int countOfTasks;
    public List<Replic> replics; 
}

[System.Serializable]
public struct Monolog
{ 
    public List<Replic> replics;
}

[System.Serializable]
public struct Replic
{
    public int eventsCount;
    public string emotion;
    public bool isPlayer;
    [TextArea(5,15)]
    public string text;
} 
#endregion