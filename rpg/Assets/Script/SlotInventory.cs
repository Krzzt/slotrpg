using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : MonoBehaviour
{
   
    public SlotSkill[] SlotsUnlocked = new SlotSkill[20];
    public SlotSkill[] currSlots = new SlotSkill[5];
    public Image[] EquippedSlotImages = new Image[5];
    public Sprite[] SpriteSlots = new Sprite[20];


    public Image[] UnlockedSlotImages = new Image[9];
    public GameObject[] UnlockedImageSlotObjects = new GameObject[9];
    public bool[] UnlockedSlots = new bool[Slots.allSlots.Count];
    public int UnlockedSlotsCount = 0;

    public GameObject SlotImageObject0;
    public GameObject SlotImageObject1;
    public GameObject SlotImageObject2;
    public GameObject SlotImageObject3;
    public GameObject SlotImageObject4;

    public int[] currSlotIDs = new int[5];

    public SlotArray SlotArray = new SlotArray();


    public GameObject UniPlayerObject;
    public UniversalSlots UniSlotsScript;

    public int[] IDsOfUnlockedSlots = new int[Slots.allSlots.Count];

    public int SelectedID;


    private int SaveID;
    // Start is called before the first frame update

    private void Awake()
    {
        UniPlayerObject = GameObject.FindWithTag("UniPlayer");
        UniSlotsScript = UniPlayerObject.GetComponent<UniversalSlots>();
        UnlockedSlots = new bool[Slots.allSlots.Count];
        IDsOfUnlockedSlots = new int[Slots.allSlots.Count];

    }
    void Start()
    {
        SlotImageObject0 = GameObject.FindWithTag("Slot0");
        SlotImageObject1 = GameObject.FindWithTag("Slot1");
        SlotImageObject2 = GameObject.FindWithTag("Slot2");
        EquippedSlotImages[0] = SlotImageObject0.GetComponent<Image>();
        EquippedSlotImages[1] = SlotImageObject1.GetComponent<Image>();
        EquippedSlotImages[2] = SlotImageObject2.GetComponent<Image>();
        SpriteSlots = UniSlotsScript.SlotSprites;

        for (int i = 0; i < UnlockedSlotImages.Length; i++)
        {
            UnlockedSlotImages[i] = UnlockedImageSlotObjects[i].GetComponent<Image>(); 
        }

        AutoFill();
    }

    public void closeMenu()
    {
        SlotArray.SlotIDs = currSlotIDs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }






    public void EquipSlot0()
    {
    
        if (SelectedID != currSlots[0].ID && SelectedID != currSlots[1].ID && SelectedID != currSlots[2].ID && SelectedID != currSlots[3].ID && SelectedID != currSlots[4].ID)
        {
            SlotArray.SlotIDs = currSlotIDs;
            currSlots[0] = Slots.allSlots[SelectedID];
            EquippedSlotImages[0].sprite = SpriteSlots[currSlots[0].ID];

        }

    }

    public void EquipSlot1()
    {
        if (SelectedID != currSlots[0].ID && SelectedID != currSlots[1].ID && SelectedID != currSlots[2].ID && SelectedID != currSlots[3].ID && SelectedID != currSlots[4].ID)
        {
            SlotArray.SlotIDs = currSlotIDs;
            currSlots[1] = Slots.allSlots[SelectedID];
            EquippedSlotImages[1].sprite = SpriteSlots[currSlots[1].ID];
        }

    }

    public void EquipSlot2()
    {
        if (SelectedID != currSlots[0].ID && SelectedID != currSlots[1].ID && SelectedID != currSlots[2].ID && SelectedID != currSlots[3].ID && SelectedID != currSlots[4].ID)
        {
            SlotArray.SlotIDs = currSlotIDs;
            currSlots[2] = Slots.allSlots[SelectedID];
            EquippedSlotImages[2].sprite = SpriteSlots[currSlots[2].ID];
        }

    }
    public void EquipSlot3()
    {
        if (SelectedID != currSlots[0].ID && SelectedID != currSlots[1].ID && SelectedID != currSlots[2].ID && SelectedID != currSlots[3].ID && SelectedID != currSlots[4].ID)
        {
            SlotArray.SlotIDs = currSlotIDs;
            currSlots[3] = Slots.allSlots[SelectedID];
            EquippedSlotImages[3].sprite = SpriteSlots[currSlots[3].ID];
        }

    }
    public void EquipSlot4()
    {
        if (SelectedID != currSlots[0].ID && SelectedID != currSlots[1].ID && SelectedID != currSlots[2].ID && SelectedID != currSlots[3].ID && SelectedID != currSlots[4].ID)
        {
            SlotArray.SlotIDs = currSlotIDs;
            currSlots[4] = Slots.allSlots[SelectedID];
            EquippedSlotImages[4].sprite = SpriteSlots[currSlots[4].ID];
        }

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


    public void SelectID(int imageID)
    {
        SelectedID = IDsOfUnlockedSlots[imageID];
    }

    public void OpenInvImages()
    {
        SaveID = -1;
       





        UnlockedSlotsCount = 0;
        //Setting everything to Inactive
        for (int i = 0; i < UnlockedImageSlotObjects.Length; i++)
        {
            UnlockedImageSlotObjects[i].SetActive(false);
        }
        //Counting the Unlocked Slots
        for (int i = 0; i < UnlockedSlots.Length; i++)
        {
            UnlockedSlots[i] = Slots.allSlots[i].Unlocked;
            if (UnlockedSlots[i])
            {
                UnlockedSlotsCount++;
                
            }

        }
        //Setting as Many Images as Unlocked to Active
        for (int i = 0; i < UnlockedSlotsCount; i++)
        {
            UnlockedImageSlotObjects[i].SetActive(true);
        }
        Debug.Log(UnlockedSlotsCount);
        //Setting the Sprites to the Images. The SaveID makes sure that every sprite is different
        for (int i = 0; i < UnlockedSlotsCount; i++) 
        {
            for (int a = 0; a < Slots.allSlots.Count; a++)
            {
                if (Slots.allSlots[a].Unlocked && Slots.allSlots[a].ID > SaveID)
                {
                    UnlockedSlotImages[i].sprite = UniSlotsScript.SlotSprites[a];
                    SaveID = Slots.allSlots[a].ID;
                    IDsOfUnlockedSlots[i] = SaveID;
                    break;
                }

            }

        }
        //man
    }




    public void UnlockSlot (int id)
    {
        Slots.UnlockSlot(id);
        Debug.Log("BOOL: " + Slots.allSlots[id].Unlocked);
    }


    public void AutoFill()
    {
        if (currSlots[0] == null)
        {
            currSlots[0] = Slots.allSlots[0];
            EquippedSlotImages[0].sprite = SpriteSlots[currSlots[0].ID];
        }
        if (currSlots[1] == null){
            currSlots[1] = Slots.allSlots[1];
            EquippedSlotImages[1].sprite = SpriteSlots[currSlots[1].ID];
        }
        if (currSlots[2] == null)
        {
            currSlots[2] = Slots.allSlots[2];
            EquippedSlotImages[2].sprite = SpriteSlots[currSlots[2].ID];
        }
        if (currSlots[3] == null)
        {
            currSlots[3] = Slots.allSlots[3];
            EquippedSlotImages[3].sprite = SpriteSlots[currSlots[3].ID];
        }
        if (currSlots[4] == null)
        {
            currSlots[4] = Slots.allSlots[4];
            EquippedSlotImages[4].sprite = SpriteSlots[currSlots[4].ID];
        }
    }
}
