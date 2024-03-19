using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class DialogStorage  
{
    private static CharachterList charachterDialogList;
    private static MonologList monologList;

    public static void setDialogsForCurrentLevel(string levelName)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/DialogsSource/" + levelName + ".json");
        charachterDialogList = JsonConvert.DeserializeObject<CharachterList>(jsonString);
    }

    public static void setMonologsForCurrentLevel(string levelName)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/DialogsSource/" + levelName + "_monologs.json");
        monologList = JsonConvert.DeserializeObject<MonologList>(jsonString);
    }

    public static Monolog getMonologByName(string monologName )
    {
        return monologList.monologs[monologName];
    }

    public static Dialog getCharachterDialog(string charachterName, int dialogNumber)
    {  
        return charachterDialogList.charachters[charachterName].dialogs[dialogNumber]; 
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
    int countOfTasks;
    public List<Replic> replics; 
}

[System.Serializable]
public struct Monolog
{ 
    public List<string> replics;
}

[System.Serializable]
public struct Replic
{
    public bool isPlayer;
    public string text;
} 
#endregion