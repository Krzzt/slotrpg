using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    public GameObject ShopOverlay;




    private void Awake()
    {
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
    }

    public void OpenSlots(int SlotType)
    {
        ShopOverlay.SetActive(false);
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
        int itemtoGet = 0;
        yield return new WaitForSeconds(0.3f);
        switch (SlotType)
        {
            case 0:
                
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




