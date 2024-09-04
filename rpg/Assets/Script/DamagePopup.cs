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

    public static DamagePopup Create( int damageAmount, bool isCrit, Vector3 position)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCrit, position);
        return damagePopup;
        

    }
    public void Setup (int damageAmount, bool isCrit, Vector3 position)
    {
        textmesh.SetText(damageAmount.ToString());
        disappearTimer = DISAPPEAR_TIMER_MAX;
        
        if (isCrit)
        {
            textmesh.fontSize = 10;
            textColor = UtilsClass.GetColorFromString("f21f1f");
        }
        else
        {
            textmesh.fontSize = 8;
            textColor = UtilsClass.GetColorFromString("ffffff");
        }
        textmesh.color = textColor;

        moveVector = new Vector3(0, 6);

        

    }

    public static DamagePopup CreateHeal(int healAmount, bool isCrit, Vector3 position)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.SetupHeal(healAmount, isCrit, position);
        return damagePopup;


    }

    public void SetupHeal(int HealAmount, bool isCrit, Vector3 position)
    {
        textmesh.SetText(HealAmount.ToString());
        disappearTimer = DISAPPEAR_TIMER_MAX;

        if (isCrit)
        {
            textmesh.fontSize = 12;
            textColor = UtilsClass.GetColorFromString("77ff00");
        }
        else
        {
            textmesh.fontSize = 8;
            textColor = UtilsClass.GetColorFromString("4ccf66");
        }
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
}
