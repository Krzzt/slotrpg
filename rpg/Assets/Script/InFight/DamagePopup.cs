using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CodeMonkey.Utils;
using UnityEngine.UIElements;

public class DamagePopup : MonoBehaviour
{
    
    private TextMeshPro textmesh;
    private float disappearTimer;
    private Color textColor;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private Vector3 moveVector;
    public Transform pfDamagePopup;
    private void Awake()
    {
      textmesh = transform.GetComponent<TextMeshPro>();
        textColor = UtilsClass.GetColorFromString("ffffff");
    }

    public static DamagePopup Create( int damageAmount, string type, Vector3 position)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount,type , position);
        return damagePopup;
        

    }
    public void Setup (int damageAmount, string Type, Vector3 position)
    {
        textmesh.SetText(damageAmount.ToString());
        disappearTimer = DISAPPEAR_TIMER_MAX;
        
        if (Type.Equals("Heal"))
        {
          
            textColor = UtilsClass.GetColorFromString("4ccf66");
        }
        else if (Type.Equals("Damage"))
        {
           
            textColor = UtilsClass.GetColorFromString("ffffff");
        }
        else if (Type.Equals("Poison"))
        {
            textColor = UtilsClass.GetColorFromString("1E9A1A");
        }
        textmesh.fontSize = 8;
        textmesh.color = textColor;

        moveVector = new Vector3(0, 6);

        

    }



    private void Update()
    {


        
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * Time.deltaTime * 0.3f;


        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textmesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

       
    }





    public static DamagePopup CreateText(string Text, Vector3 position)
    {
        Transform transform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup textToCreate = transform.GetComponent<DamagePopup>();
        textToCreate.SetupText(Text, position);
        return textToCreate;
    }


    public void SetupText(string Text, Vector3 position)
    {
        textmesh.SetText(Text);
        disappearTimer = DISAPPEAR_TIMER_MAX;
        textmesh.fontSize = 8; 
        textColor = UtilsClass.GetColorFromString("ffffff");
        textmesh.color = textColor;

        moveVector = new Vector3(0,6);
    }
}
