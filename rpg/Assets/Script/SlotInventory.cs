using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : MonoBehaviour
{
   
    public SlotSkill[] SlotsUnlocked = new SlotSkill[20];
    public SlotSkill[] currSlots = new SlotSkill[3];
    public Sprite[] SlotsImages = new Sprite[20];
    public Image[] EquippedSlotImages = new Image[3];
    // Start is called before the first frame update

    private void Awake()
    {
        EquipSlot(0, 0);
        EquipSlot(1, 1);
        EquipSlot(2, 2);
        EquippedSlotImages[0].sprite = SlotsImages[currSlots[0].ID];
        EquippedSlotImages[1].sprite = SlotsImages[currSlots[1].ID];
        EquippedSlotImages[2].sprite = SlotsImages[currSlots[2].ID];


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EquipSlot(int ID, int slotNumber)
    {
        SlotsUnlocked[ID] = currSlots[slotNumber];
    }
}
