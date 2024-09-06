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


    // Update is called once per frame

    private void Awake()
    {
        Camera = GameObject.FindWithTag("MainCamera");
        sceneCamera = Camera.GetComponent<Camera>();
        InvMenu = GameObject.FindWithTag("test");
        inventoryScript = InvMenu.GetComponent<SlotInventory>();
       InvMenu.SetActive(false);



    }
    void Update()
    {
       
            ProcessInputs();
            move();
        if (Input.GetKeyDown("e") && !invIsActive)
        {
            invIsActive = true;
            InvMenu.SetActive(true);
            inventoryScript.OpenInvImages();
        }
        else if (Input.GetKeyDown("e") && invIsActive)
        {
            invIsActive = false;
            InvMenu.SetActive(false);
            inventoryScript.AutoFill();
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
}

