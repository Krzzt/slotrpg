using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    public GameObject Camera;

    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    public Camera sceneCamera;

    public GameObject InvMenu;
    public bool invIsActive = false;
    public SlotInventory inventoryScript;

    public GameObject PickUpTextObject;


    public bool canPickUpSlotObject;
    public GameObject SlotToPickUp;
    

    // Update is called once per frame

    private void Awake()
    {
        Camera = GameObject.FindWithTag("MainCamera");
        sceneCamera = Camera.GetComponent<Camera>();
        InvMenu = GameObject.FindWithTag("test");
        inventoryScript = InvMenu.GetComponent<SlotInventory>();
        PickUpTextObject = GameObject.FindWithTag("PickUpText");
       InvMenu.SetActive(false);
        PickUpTextObject.SetActive(false);



    }
    void Update()
    {
       
            ProcessInputs();
            move();
        if (Input.GetKeyDown("i") && !invIsActive)
        {
            invIsActive = true;
            InvMenu.SetActive(true);
            inventoryScript.OpenInvImages();
            inventoryScript.AutoFill();
        }
        else if (Input.GetKeyDown("i") && invIsActive)
        {
            invIsActive = false;
            InvMenu.SetActive(false);
            inventoryScript.AutoFill();
            inventoryScript.saveSlots();
            inventoryScript.closeMenu();
        }
        
        if (Input.GetKeyDown("e"))
        {
            if (canPickUpSlotObject)
            {
                Debug.Log("Tag is: " + SlotToPickUp.tag);
                PickUpSlotItem(SlotToPickUp);
            }
        } 
      



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            Debug.Log("ENTERED");
            SlotToPickUp = collision.gameObject;
            
            PickUpTextObject.SetActive(true);
            canPickUpSlotObject = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "PickUp")
        {
            SlotToPickUp = collision.gameObject;
            PickUpTextObject.SetActive(true);
            canPickUpSlotObject = true;
        }
   
    
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            canPickUpSlotObject = false;
            PickUpTextObject.SetActive(false);
            SlotToPickUp = null;
            Debug.Log("EXITED");
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(moveX, moveY).normalized;


    }
    void move()
    {

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);


    }


    void PickUpSlotItem(GameObject ObjectToPickUp)
    {
        Debug.Log("Object has tag: " + ObjectToPickUp.tag);
        PickUpItemLogic Item = ObjectToPickUp.GetComponent<PickUpItemLogic>();
        Item.PickUpSlot();
    }
}

