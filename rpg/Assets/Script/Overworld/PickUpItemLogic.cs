using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class PickUpItemLogic : MonoBehaviour
{
    public int SlotID;
    public GameObject PlayerObject;
    public string TextFromThisObject;

    private void Awake()
    {
        PlayerObject = GameObject.FindWithTag("PlayerOW");
    }
    public void PickUpSlot()
    {
        Slots.UnlockSlot(SlotID);
        PickUpMessage();
        gameObject.SetActive(false);
        
    }

    public void PickUpMessage()
    {
        Transform positiontoSpawn = PlayerObject.transform;
        GameObject TextBox = Instantiate(GameAssets.i.TextBox, positiontoSpawn.position , Quaternion.identity);
        TMP_Text textToSet = TextBox.GetComponentInChildren<TMP_Text>();
        textToSet.SetText("Player has obtained " + TextFromThisObject);

    }


}
