using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInEncounter : MonoBehaviour
{
 

    public UnitHealth PlayerHealth = new UnitHealth(0, 0);
    public float DamageToDeal;


    public GameObject Buttons;
    public GameObject SelectEnemy;

    public GameObject targetEnemy;


    public bool isAttackMode = false;
    public bool isSlotMode = false;


    public Transform DamagePopupTransform;

    public GameObject PlayerHealthTextObject;
    public TMP_Text PlayerHealthText;
    public Sprite[] SlotImages = new Sprite[20];
    public Image[] equipSlotImages = new Image[5];
    public SlotSkill[] equipSlots = new SlotSkill[5];
    public Image Slot0;
    public Image Slot1;
    public Image Slot2;
    public SlotSkill[] rolledSlots = new SlotSkill[3];

    public GameObject UniPlayerGameObject;
    public UniversalSlots UniversalSlots;
    public GameObject SlotObject;

    public SlotArray slots;

    public Transform playerpopupTransform;

    public GameObject ContButton;

    public int[] nextHitEffects = new int[10];

    public float InFightDefenseBooster;
    public float InFightDamageBooster;

    public Player player = new Player
    {
        level = 1,
        exp = 0,
        expToNextLevel = 100,
        AttackDamage = 5,
        MaxHealth = 50,
        currentHealth = 50,
        Defense = 1,
        Initiative = 8,

    };

    int[] IDCount = new int[Slots.allSlots.Count];

    public TMP_Text[] effectTexts = new TMP_Text[3];


    public Sprite setBlack;


    // Start is called before the first frame update
    void Awake()
    {
        SaveSystem.checkIfExists("/Player.txt");
        SaveSystem.LoadPlayer(player);
        PlayerHealth.addmaxHealth(player.MaxHealth);
        PlayerHealth._currentHealth = player.currentHealth;
        SlotObject.SetActive(false);
        Buttons.SetActive(false);
        SelectEnemy.SetActive(false);
        PlayerHealthTextObject = GameObject.FindWithTag("HealthText");
        PlayerHealthText = PlayerHealthTextObject.GetComponent<TMP_Text>();
        slots = new SlotArray();
        SaveSystem.checkIfExists("/saveSlotIcons.txt");
        SaveSystem.LoadSlotIcons(slots);

        for (int i = 0; i < Slots.allSlots.Count; i++)
        {
            equipSlots[i] = Slots.allSlots[slots.SlotIDs[i]];
        }
        UniPlayerGameObject = GameObject.FindWithTag("UniPlayer");
        UniversalSlots = UniPlayerGameObject.GetComponent<UniversalSlots>();
        SlotImages = UniversalSlots.SlotSprites;
        ContButton.SetActive(false);
        DamageToDeal = player.AttackDamage;


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
        nextHitEffects = new int[10];
        DamageToDeal = player.AttackDamage;
        Buttons.SetActive(true);

    }


    public void SlotMode()
    {
        isSlotMode = true;
        SlotObject.SetActive(true);
        StartCoroutine(Slotstart());
        //spin
        //have effect with current slots

    }


    IEnumerator Slotstart()
    {
        yield return new WaitForSeconds(0.3f);
        int slot0 = Random.Range(0, 5);
        int slot1 = Random.Range(0, 5);
        int slot2 = Random.Range(0, 5);
        rolledSlots[0] = equipSlots[slot0];
        rolledSlots[1] = equipSlots[slot1];
        rolledSlots[2] = equipSlots[slot2];
        Debug.Log("ID FOR 0: " + rolledSlots[0].ID);
        Debug.Log("ID FOR 1: " + rolledSlots[1].ID);
        Debug.Log("ID FOR 2: " + rolledSlots[2].ID);
        Slot0.sprite = SlotImages[rolledSlots[0].ID];
        yield return new WaitForSeconds(0.3f);
        Slot1.sprite = SlotImages[rolledSlots[1].ID];
        yield return new WaitForSeconds(0.3f);
        Slot2.sprite = SlotImages[rolledSlots[2].ID];
        ApplyEffects();
        ContButton.SetActive(true);
        SetEffectTexts();




    }

    public void ApplyEffects()
    {
        List<SlotSkill> rolledSlotList = rolledSlots.ToList<SlotSkill>();
        IDCount = new int[Slots.allSlots.Count];
        for (int i = 0; i < rolledSlotList.Count; i++)
        {
            IDCount[rolledSlotList[i].ID]++;
        }

        //Sword
        if (IDCount[0] == 1)
        {
            DamageToDeal *= 1.05f;
        }
        else if (IDCount[0] == 2)
        {
            DamageToDeal *= 1.1f;
        }
        else if (IDCount[0] == 3)
        {
            DamageToDeal *= 1.3f;
        }
        //Heart
        if (IDCount[1] == 1)
        {
            PlayerHealth.HealUnit((int)(PlayerHealth._currentMaxHealth * 0.03f));
            PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        }
        else if (IDCount[1] == 2)
        {
            PlayerHealth.HealUnit((int)(PlayerHealth._currentMaxHealth * 0.08f));
            PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        }
        else if (IDCount[1] == 3)
        {
            PlayerHealth.HealUnit((int)(PlayerHealth._currentMaxHealth * 0.15f));
            PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        }
        //Poison
        if (IDCount[2] == 1)
        {
            //poison1
            nextHitEffects[0] = 1;
        }
        else if (IDCount[2] == 2)
        {
            //poison2
            nextHitEffects[0] = 2;
        }
        else if (IDCount[2] == 3)
        {
            //poison4
            nextHitEffects[0] = 4;
        }
        //Shield
        if (IDCount[3] == 1)
        {
            InFightDefenseBooster += 0.05f;
        }
        else if (IDCount[3] == 2)
        {
            InFightDefenseBooster += 0.1f;
        }
        else if (IDCount[3] == 3)
        {
            InFightDefenseBooster += 0.4f;
        }
        //Stun
        if (IDCount[4] == 1)
        {
            //nothing
        }
        else if (IDCount[4] == 2)
        {
            nextHitEffects[1] = 1;
        }
        else if (IDCount[4] == 3)
        {
            nextHitEffects[1] = 3;
        }

    }

    public void ClearEverything()
    {
        Slot0.sprite = setBlack;
        Slot1.sprite = setBlack;
        Slot2.sprite = setBlack;
        effectTexts[0].SetText("");
        effectTexts[1].SetText("");
        effectTexts[2].SetText("");
    }

    public void SetEffectTexts()
    {
        int index = 0;
        for (int i = 0; i < IDCount.Length; i++)
        {
            if (IDCount[i] == 1)
            {
                effectTexts[index].SetText(Slots.allSlots[i].Desc1);
                index++;
            }
            else if (IDCount[i] == 2)
            {
                effectTexts[index].SetText(Slots.allSlots[i].Desc2);
                index++;
            }
            else if (IDCount[i] == 3)
            {
                effectTexts[index].SetText(Slots.allSlots[i].Desc3);
                index++;
            }
            
        }
    }
    public void AttackMode()
    {
        isAttackMode = true;
        Buttons.SetActive(false);
        SlotObject.SetActive(false);
        SelectEnemy.SetActive(true);
    }
    
    public int GetInitiative() { return player.Initiative; }
    public int GetHealth() { return PlayerHealth._currentHealth; }


    public void DamagePlayer(int amount,string type)
    {

        amount -= (int)(player.Defense * InFightDefenseBooster);
        if (amount <= 0)
        {
            amount = 1;
        }
        PlayerHealth.DamageUnit(amount);
        DamagePopup.Create(amount, type, playerpopupTransform.position);
        PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        
    }

    public void LevelUp()
    {
        player.exp -= player.expToNextLevel;
        player.level++;
        player.expToNextLevel = (int)(player.expToNextLevel * 1.5f);
        DamagePopup.CreateLvlUp(playerpopupTransform.position);



        player.AttackDamage = (int)(player.AttackDamage * 1.2f);
        player.MaxHealth = (int)(player.MaxHealth * 1.1f);
        player.currentHealth = (int)(player.currentHealth + ((int)(player.MaxHealth * 0.1f)));
        if (player.level % 2 == 0)
        {
            player.Defense++;
        }

        player.Initiative += 2;
        

    }
    
}

public class SlotArray
{
    public int[] SlotIDs;
}


public class Player
{
    public int level;
    public int exp;
    public int expToNextLevel;
    public int AttackDamage;
    public int MaxHealth;
    public int currentHealth;
    public int Defense;
    public int Initiative;
}
