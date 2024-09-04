using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInEncounter : MonoBehaviour
{
    
    public UnitHealth PlayerHealth = new UnitHealth(0, 0);
    public int MaxHealth;
    public int InitiativeBonus;
    public int AttackDamage = 5;
    public int Defense;

    public int Initiative;

    public GameObject Buttons;
    public GameObject SelectEnemy;

    public GameObject targetEnemy;


    public bool isAttackMode = false;
    public bool isSlotMode = false;


    public Transform DamagePopupTransform;

    public GameObject PlayerHealthTextObject;
    public TMP_Text PlayerHealthText;
    public Image[] SlotImages = new Image[20];

 


    // Start is called before the first frame update
    void Awake()
    {
        PlayerHealth.addmaxHealth(MaxHealth);
        PlayerHealth._currentHealth = MaxHealth;
        Initiative = Random.Range(49, 50);
        Buttons.SetActive(false);
        SelectEnemy.SetActive(false);
        PlayerHealthTextObject = GameObject.FindWithTag("HealthText");
        PlayerHealthText = PlayerHealthTextObject.GetComponent<TMP_Text>();
     

    }
    private void Start()
    {
        PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnStart()
    {
        Buttons.SetActive(true);

    }


    public void SlotMode()
    {
        isSlotMode = true;

    }

    public void AttackMode()
    {
        isAttackMode = true;
        Buttons.SetActive(false);
        SelectEnemy.SetActive(true);
    }
    
    public int GetInitiative() { return Initiative; }
    public int GetHealth() { return PlayerHealth._currentHealth; }


    public void DamagePlayer(int amount)
    {
        PlayerHealth.DamageUnit(amount);
        DamagePopup.Create(amount, false, DamagePopupTransform.position);
        PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        
    }

    
}
