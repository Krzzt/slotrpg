using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Slots {


    public static List<SlotSkill> allSlots = new List<SlotSkill>{

        new SlotSkill { Name = "Sword" , ID = 0, Unlocked = true , Desc1 = "Damage Multiplier x1.05", Desc2 = "Damage Multiplier x1.10", Desc3 = "Damage Multiplier x1.30" },
        new SlotSkill { Name = "Heart" ,ID = 1, Unlocked = true, Desc1 = "Heal by 3%", Desc2 = "Heal by 8%", Desc3 = "Heal by 15%"  },
        new SlotSkill { Name = "Poison" , ID = 2, Unlocked = true, Desc1 = "Poison 1 on next Hit",Desc2 = "Poison 2 on next Hit",Desc3 = "Poison 4 on next Hit",},
        new SlotSkill { Name = "Shield",ID = 3, Unlocked = true, Desc1 = "+5% Defense for the Fight",Desc2 = "+10% Defense for the Fight", Desc3 = "+40% Defense for the Fight"  },
        new SlotSkill { Name = "Stun", ID = 4, Unlocked = true, Desc1 = "Nothing",Desc2 = "1 Round Stun for 1 Enemy", Desc3 = "3 Round Stun for 1 Enemy" }







        };


    public static void UnlockSlot(int id)
    {
        allSlots[id].Unlocked = true;
        Debug.Log("Slot with ID " + allSlots[id] + " Unlocked");
    }


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
