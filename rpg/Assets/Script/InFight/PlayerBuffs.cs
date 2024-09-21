using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerBuffs
{
    public static List<Buff> Conditions = new List<Buff>
    {
        new Buff {Name = "Dodge", ID = 0  },
    };
}


public class Buff
{
    public string Name { get; set; }
    public int ID { get; set; }
}