using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Slots {


    public static List<SlotSkill> allSlots = new List<SlotSkill>{

        new SlotSkill { Name = "Sword" , ID = 0, Unlocked = true , Desc1 = "Damage Multiplier x1.05", Desc2 = "Damage Multiplier x1.10", Desc3 = "Damage Multiplier x1.30" },
        new SlotSkill { Name = "Heart" ,ID = 1, Unlocked = true, Desc1 = "Heal by 3%", Desc2 = "Heal by 8%", Desc3 = "Heal by 15%"  },
        new SlotSkill { Name = "Poison" , ID = 2, Unlocked = true, Desc1 = "Poison 1 on next Hit",Desc2 = "Poison 2 on next Hit",Desc3 = "Poison 4 on next Hit",},
        new SlotSkill { Name = "Shield",ID = 3, Unlocked = true, Desc1 = "+5% Defense for the Fight",Desc2 = "+10% Defense for the Fight", Desc3 = "+20% Defense for the Fight + 10% Reflection Damage"  },
        new SlotSkill { Name = "Stun", ID = 4, Unlocked = true, Desc1 = "Nothing",Desc2 = "1 Round Stun for 1 Enemy", Desc3 = "3 Round Stun for 1 Enemy" },
        new SlotSkill { Name = "Shadow", ID = 5, Unlocked = false, Desc1 = "Steal 5% of the Damage done", Desc2 = "Steal 10% of the Damage done", Desc3 = "Dodge 1 and Steal 20% of the Damage done" }







        };


    public static void UnlockSlot(int id)
    {
     

        UnlockedSlots slotsToLoad = new UnlockedSlots { unlockedSlots = new bool[allSlots.Count] };

        SaveSystem.checkIfExists("/UnlockedSlots.txt");
        SaveSystem.LoadUnlockedSlots(slotsToLoad);

        if (!slotsToLoad.unlockedSlots[0])
        {
            for (int i = 0; i < 5; i++)
            {
                slotsToLoad.unlockedSlots[i] = true;
            }
        }
        slotsToLoad.unlockedSlots[id] = true;
        Debug.Log("Slot with ID " + allSlots[id].ID + " Unlocked");
        SaveSystem.SaveUnlockedSlots(slotsToLoad);
    }


}


public class UnlockedSlots
{
    public bool[] unlockedSlots;
}
public class SlotSkill
{
    public string Name
    {
        get;
        set;
    }
    public string Desc1
    {
        get;
        set;
    }
    public string Desc2
    {
        get;
        set;
    }
    public string Desc3
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
