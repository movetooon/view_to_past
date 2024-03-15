using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class DialogStorage  
{
    private static CharachterList charachterDialogList;

    public static void setDialogsForCurrentLevel(string levelName)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/DialogsSource/" + levelName + ".json");
        charachterDialogList = JsonConvert.DeserializeObject<CharachterList>(jsonString);
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


[System.Serializable] 
struct Charachter
{ 
    public List<Dialog> dialogs;  
}


[System.Serializable]
public struct Dialog
{ 
    public List<Replic> replics; 
}


[System.Serializable]
public struct Replic
{
    public bool isPlayer;
    public string text;
} 
#endregion