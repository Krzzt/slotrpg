using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using Newtonsoft.Json;


public static class SaveSystem
{
    public static void checkIfExists(string dataPath)
    {
        if (!File.Exists(Application.dataPath + dataPath))
        {
            File.Create(Application.dataPath + dataPath);
        }

    }
    public static void SaveBiome(Biomes biome)
    {
        

        string biomeJson = JsonUtility.ToJson(biome);
        File.WriteAllText(Application.dataPath + "/saveBiome.txt", biomeJson);
    }

    public static void LoadBiomeforFight(Biomes biomeToLoadTo)
    {
        string loadBiomeString = File.ReadAllText(Application.dataPath + "/saveBiome.txt");
        JsonUtility.FromJsonOverwrite(loadBiomeString, biomeToLoadTo);
       

    }



    public static void SaveSlotIcons(SlotArray SlotsToSave)
    {
     
        string SlotJson = JsonUtility.ToJson(SlotsToSave);
        File.WriteAllText(Application.dataPath + "/saveSlotIcons.txt", SlotJson);
       
    }

    public static void LoadSlotIcons(SlotArray SlotArrayToLoad)
    {
        string loadSlots = File.ReadAllText(Application.dataPath + "/saveSlotIcons.txt");
        JsonUtility.FromJsonOverwrite(loadSlots, SlotArrayToLoad);
    }




    public static void SavePos(positionSave posToSave)
    {
        string transToSaveString = JsonUtility.ToJson(posToSave);
        File.WriteAllText(Application.dataPath + "/savePos.txt", transToSaveString);
    }

    public static void LoadPos (positionSave posToLoad)
    {
        string loadTransform = File.ReadAllText(Application.dataPath + "/savePos.txt");
        JsonUtility.FromJsonOverwrite(loadTransform, posToLoad);
    }
}
