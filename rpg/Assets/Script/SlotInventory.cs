using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : MonoBehaviour
{
   
    public SlotSkill[] SlotsUnlocked = new SlotSkill[20];
    public SlotSkill[] currSlots = new SlotSkill[3];
    public Image[] EquippedSlotImages = new Image[3];
    public Sprite[] SpriteSlots = new Sprite[20];


    public GameObject SlotImageObject0;
    public GameObject SlotImageObject1;
    public GameObject SlotImageObject2;

    public int[] currSlotIDs = new int[3];

    public SlotArray SlotArray = new SlotArray();
    // Start is called before the first frame update

    private void Awake()
    {


    }
    void Start()
    {
        SlotImageObject0 = GameObject.FindWithTag("Slot0");
        SlotImageObject1 = GameObject.FindWithTag("Slot1");
        SlotImageObject2 = GameObject.FindWithTag("Slot2");
        EquippedSlotImages[0] = SlotImageObject0.GetComponent<Image>();
        EquippedSlotImages[1] = SlotImageObject1.GetComponent<Image>();
        EquippedSlotImages[2] = SlotImageObject2.GetComponent<Image>();
        

        EquipSlot(0, 0);
        EquipSlot(1, 1);
        EquipSlot(2, 2);
        EquippedSlotImages[0].sprite = SpriteSlots[currSlots[0].ID];
        EquippedSlotImages[1].sprite = SpriteSlots[currSlots[1].ID];
        EquippedSlotImages[2].sprite = SpriteSlots[currSlots[2].ID];
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void EquipSlot(int ID, int slotNumber)
    {


        SlotArray.SlotIDs = currSlotIDs;
        SlotsUnlocked = Slots.allSlots.ToArray();
        currSlots[slotNumber] = SlotsUnlocked[ID];
    }

    public void saveSlots()
    {
        for (int i = 0; i < currSlots.Length; i++)
        {
            currSlotIDs[i] = currSlots[i].ID;
        }

        SlotArray.SlotIDs = currSlotIDs;
        Debug.Log("TEST TEXT " + JsonUtility.ToJson(Slots.allSlots[0]));
        SaveSystem.checkIfExists("saveSlotIcons.txt");
        SaveSystem.SaveSlotIcons(SlotArray);
    }
}
