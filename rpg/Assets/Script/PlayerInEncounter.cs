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
    public Image[] equipSlotImages = new Image[3];
    public SlotSkill[] equipSlots = new SlotSkill[3];
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

        for (int i = 0; i < slots.SlotIDs.Length; i++)
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
        int slot0 = Random.Range(0, 3);
        int slot1 = Random.Range(0, 3);
        int slot2 = Random.Range(0, 3);
        rolledSlots[0] = equipSlots[slot0];
        rolledSlots[1] = equipSlots[slot1];
        rolledSlots[2] = equipSlots[slot2];
        Slot0.sprite = SlotImages[rolledSlots[0].ID];
        yield return new WaitForSeconds(0.3f);
        Slot1.sprite = SlotImages[rolledSlots[1].ID];
        yield return new WaitForSeconds(0.3f);
        Slot2.sprite = SlotImages[rolledSlots[2].ID];
        ApplyEffects();
        ContButton.SetActive(true);




    }

    public void ApplyEffects()
    {
        List<SlotSkill> rolledSlotList = rolledSlots.ToList<SlotSkill>();
        int[] IDCount = new int[Slots.allSlots.Count];
        for (int i = 0; i < rolledSlotList.Count; i++)
        {
            IDCount[rolledSlotList[i].ID]++;
        }
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
        if (IDCount[1] == 1)
        {
            PlayerHealth.HealUnit((int)(PlayerHealth._currentMaxHealth * 0.05f));
            PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        }
        else if (IDCount[1] == 2)
        {
            PlayerHealth.HealUnit((int)(PlayerHealth._currentMaxHealth * 0.10f));
            PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        }
        else if (IDCount[1] == 3)
        {
            PlayerHealth.HealUnit((int)(PlayerHealth._currentMaxHealth * 0.30f));
            PlayerHealthText.SetText("Health: " + PlayerHealth._currentHealth + "/" + PlayerHealth._currentMaxHealth);
        }
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
        player.Defense++;
        player.Initiative += 3;
        

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
