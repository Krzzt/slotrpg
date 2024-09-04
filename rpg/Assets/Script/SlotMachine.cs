using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{

    public SlotSkill[] equipedSlots = new SlotSkill[3];






    public List<SlotSkill> UnlockedSlots = new List<SlotSkill>();
   

    public void Awake()
    {
        UnlockedSlots.Add(Slots.allSlots[0]);
        UnlockedSlots.Add(Slots.allSlots[1]);
        UnlockedSlots.Add(Slots.allSlots[2]);
        SaveSystem.LoadSlotIcons(equipedSlots);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



