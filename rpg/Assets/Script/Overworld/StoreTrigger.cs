using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StoreTrigger
{
    public static List<StoreObject> StoreObjects = new List<StoreObject>
    {
        new StoreObject{ID = 0, SlotIconIDs = new List<int>{6,7 }},

    };
}




public class StoreObject
{
    public int ID;
    public List<int> SlotIconIDs;
}