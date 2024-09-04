using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Slots {


    public static List<SlotSkill> allSlots = new List<SlotSkill>{

        new SlotSkill { Name = "Sword" , Description = "This mighty Sword will Increase your Damage", ID = 0},
        new SlotSkill { Name = "Heart" , Description = "This cute Heart will Heal you!", ID = 1},
        new SlotSkill { Name = "Poison" , Description = "This Drop of Poison will Inflict Poison on the Enemy", ID = 2}






        };





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
}
