using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Slots {


    public static List<SlotSkill> allSlots = new List<SlotSkill>{

        new SlotSkill { Name = "Sword" , Description = "This mighty Sword will Increase your Damage", ID = 0, Unlocked = true },
        new SlotSkill { Name = "Heart" , Description = "This cute Heart will Heal you!", ID = 1, Unlocked = true },
        new SlotSkill { Name = "Poison" , Description = "This Drop of Poison will Inflict Poison on the Enemy", ID = 2, Unlocked = true},
        new SlotSkill { Name = "unlocktest", Description = "test", ID = 3, Unlocked = false }
        






        };


    public static void UnlockSlot(int id)
    {
        allSlots[id].Unlocked = true;
    }


}
public class SlotSkill
{
    public string Name
    {
        get;
        set;
    }
    public string Description
    {
        get;
        set;
    }
    public int ID
    {
        get;
        set;
    }

    public bool Unlocked
    {
        get;
        set;
    }
}
