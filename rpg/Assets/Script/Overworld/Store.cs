using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public GameObject SlotMachine;
    public GameObject ContinueButton;

    private GameObject UniPlayerObject;
    private UniversalSlots UniPlayerScript;
    private Player PlayerToLoad;

    public GameObject ShopOverlay;


    public int[] SlotIconIDs = new int[2];

    public Image[] SlotMachineImages = new Image[3];
    public TMP_Text UnlockedText;

    private void Awake()
    {
        SlotMachine = GameObject.FindWithTag("ShopSlot");
        ContinueButton = GameObject.FindWithTag("ShopContinue");
        UniPlayerObject = GameObject.FindWithTag("UniPlayer");
        UniPlayerScript = UniPlayerObject.GetComponent<UniversalSlots>();
        SaveSystem.LoadPlayer(PlayerToLoad);

        ShopOverlay.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateShop();
    }


    public void ActivateShop()
    {
        ShopOverlay.SetActive(true);
        SlotMachine.SetActive(false); 
    }

    public void checkForMoney(int SlotType)
    {

    }

    public void OpenSlots(int SlotType)
    {
        ShopOverlay.SetActive(false);
        SlotMachine.SetActive(true);
        //0 = IconLootBox
        //1 = TrinketLootBox
        //2 = CharmLootBox
        //3 = SpellLootBox
        //4 =
        //5 =

        StartCoroutine(SlotSpin(SlotType));



                
        }

    IEnumerator SlotSpin(int SlotType)
    {
        int itemtoGet = -1;
        yield return new WaitForSeconds(0.3f);
        switch (SlotType)
        {
            case 0:
                itemtoGet = Random.Range(0, SlotIconIDs.Length);
                SlotMachineImages[0].sprite = UniPlayerScript.SlotSprites[SlotIconIDs[itemtoGet]];
                yield return new WaitForSeconds(0.2f);
                SlotMachineImages[1].sprite = UniPlayerScript.SlotSprites[SlotIconIDs[itemtoGet]];
                yield return new WaitForSeconds(0.2f);
                SlotMachineImages[2].sprite = UniPlayerScript.SlotSprites[SlotIconIDs[itemtoGet]];
                Slots.UnlockSlot(SlotIconIDs[itemtoGet]);
                UnlockedText.SetText("Unlocked " + Slots.allSlots[SlotIconIDs[itemtoGet]].Name);
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;



        }
    }

}




