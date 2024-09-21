using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusEffects
{
    public static List<Condition> Conditions = new List<Condition>
    {
        new Condition{Name = "Poison" , ID = 0},
        new Condition{Name = "Stun", ID = 1}
    };
}

public class Condition
{
    public string Name { get; set; }
    public int ID {  get; set; }
}
