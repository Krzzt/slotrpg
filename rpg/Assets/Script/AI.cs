using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu]
public class AI : ScriptableObject
{
    // Start is called before the first frame update
    public bool isAttacking;
    public bool isHealing;
    public bool Attack
    {
        get
        {
            return isAttacking;
        }
        set
        {
            isAttacking = value;
        }
    }
    public bool Heal
    {
        get
        {
            return isHealing;
        }
        set
        {
            isHealing = value;
        }
    }
}
