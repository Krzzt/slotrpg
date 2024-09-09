using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Enemy : MonoBehaviour
{

    public GameObject PlayerObject;
    public PlayerInEncounter Player;

    public GameObject FightManagerObject;
    public FightManager FightManager;

    public int expValue;

    public int AttackDamage;
    public int HealAmount;

    public UnitHealth EnemyHealth = new UnitHealth(5, 5);
    public int IncreaseMaxHealth;
    public int? Initiative;

    public AI usingAI;

    public Transform DamagePopupTransform;
    public Transform EnemyPopUpTransform;

    public bool[] FightOptionBool = new bool[2];



    // Start is called before the first frame update
    void Awake()
    {
        EnemyHealth.addmaxHealth(IncreaseMaxHealth);
        EnemyHealth._currentHealth = EnemyHealth._currentMaxHealth;
        Initiative = Random.Range(0, 50);
        PlayerObject = GameObject.FindWithTag("Player");
        Player = PlayerObject.GetComponent<PlayerInEncounter>();
        FightManagerObject = GameObject.FindWithTag("FightManager");
        FightManager = FightManagerObject.GetComponent<FightManager>();
        DamagePopupTransform = GameObject.FindWithTag("PlayerPopup").transform;

        FightOptionBool[0] = usingAI.isAttacking;
        FightOptionBool[1] = usingAI.isHealing;

    }

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnStart()
    {

        int random = Random.Range(0, FightOptionBool.Length);
        while (!FightOptionBool[random])
        {
            random = Random.Range(0, FightOptionBool.Length);
        }
        if (FightOptionBool[random])
        {

            switch (random)
            {
                case 0: Attack(); FightManager.NextTurn(); break;
                case 1: Heal(); FightManager.NextTurn(); break;
            }
        }

    }

    public void OnMouseDown()
    {
       
        if (Player.isAttackMode)
        {
            Player.targetEnemy = gameObject;
            Player.DamagePopupTransform = this.gameObject.transform.GetChild(0);
            float damageDealt = ((float)Player.DamageToDeal * (float)((float)Random.Range(900, 1301) / 1000f));
            DamagePopup.Create((int)damageDealt, false, Player.DamagePopupTransform.position);
            EnemyHealth.DamageUnit((int)damageDealt);
            Player.SelectEnemy.SetActive(false);
            Player.isAttackMode = false;
            if (EnemyHealth._currentHealth <= 0)
            {
                Dead();
            }
            FightManager.NextTurn();
        }
    }
    public void Attack()
    {
        float damageDealt = ((float)AttackDamage * (float)((float)Random.Range(900, 1201) / 1000f));
        Player.DamagePlayer((int)damageDealt);
        if (Player.PlayerHealth._currentHealth <= 0)
        {
            Destroy(PlayerObject);
            //fucking Die
        }
    }

    public void Heal()
    {
        int decider = Random.Range(0, 7);
        if (decider <= 1 && EnemyHealth._currentHealth < EnemyHealth._currentMaxHealth)
        {
            EnemyHealth.HealUnit(HealAmount);
        }
        else if(decider <= 1 && EnemyHealth._currentHealth >= EnemyHealth._currentMaxHealth)
        {
            List<Enemy> TempEnemyList = new List<Enemy>();
            TempEnemyList = FightManager.EnemyList;
            GameObject EnemyLowHP = TempEnemyList[0].gameObject;
            Enemy EnemyLowHPScript = TempEnemyList[0];
            int curMinHP = TempEnemyList[0].EnemyHealth._currentHealth;
            for (int i = 0; i < TempEnemyList.Count; i++)
            {
                if (TempEnemyList[i].EnemyHealth._currentHealth > 0 && TempEnemyList[i].EnemyHealth._currentHealth < curMinHP)
                {
                    curMinHP = TempEnemyList[i].EnemyHealth._currentHealth;
                    EnemyLowHP = TempEnemyList[i].gameObject;
                    EnemyLowHPScript = TempEnemyList[i];
                }
            }
            EnemyPopUpTransform = EnemyLowHP.transform.GetChild(0);
            DamagePopup.CreateHeal(HealAmount, false, EnemyPopUpTransform.position);
            EnemyLowHPScript.EnemyHealth.HealUnit(HealAmount);


            
        }
        else if (decider <= 5 && decider > 1)
        {
            List<Enemy> TempEnemyList = new List<Enemy>();
            TempEnemyList = FightManager.EnemyList;
            GameObject EnemyLowHP = TempEnemyList[0].gameObject;
            Enemy EnemyLowHPScript = TempEnemyList[0];
            int curMinHP = TempEnemyList[0].EnemyHealth._currentHealth;
            for (int i = 0; i < TempEnemyList.Count; i++)
            {
                if (TempEnemyList[i].EnemyHealth._currentHealth > 0 && TempEnemyList[i].EnemyHealth._currentHealth < curMinHP)
                {
                    curMinHP = TempEnemyList[i].EnemyHealth._currentHealth;
                    EnemyLowHP = TempEnemyList[i].gameObject;
                    EnemyLowHPScript = TempEnemyList[i];
                }
            }
            EnemyPopUpTransform = EnemyLowHP.transform.GetChild(0);
            DamagePopup.CreateHeal(HealAmount, false, EnemyPopUpTransform.position);
            EnemyLowHPScript.EnemyHealth.HealUnit(HealAmount);


        }
        else
        {
         
                List<Enemy> TempEnemyList = new List<Enemy>();
                TempEnemyList = FightManager.EnemyList;
                int tempRandomDecider = Random.Range(0, TempEnemyList.Count);
                while (TempEnemyList[tempRandomDecider].EnemyHealth._currentHealth <= 0)
                {
                    tempRandomDecider = Random.Range(0, TempEnemyList.Count);
                }
                    GameObject EnemyLowHP = TempEnemyList[tempRandomDecider].gameObject;
                    Enemy EnemyLowHPScript = TempEnemyList[tempRandomDecider];
                    EnemyPopUpTransform = EnemyLowHP.transform.GetChild(0);
                    DamagePopup.CreateHeal(HealAmount, false, EnemyPopUpTransform.position);
                    EnemyLowHPScript.EnemyHealth.HealUnit(HealAmount);
        }

    }

    public void Dead()
    {
        Initiative = null;
        gameObject.SetActive(false);
    }

    public int? GetInitiative() { return Initiative; }
    public int GetHealth() { return EnemyHealth._currentHealth; }
}



