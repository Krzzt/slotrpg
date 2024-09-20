using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public int level;

    public GameObject PlayerObject;
    public PlayerInEncounter Player;

    public GameObject FightManagerObject;
    public FightManager FightManager;

    public int expValue;

    public int AttackDamage;
    public int HealAmount;
    public int Defense;

    public UnitHealth EnemyHealth = new UnitHealth(5, 5);
    public int IncreaseMaxHealth;
    public int? Initiative;

    public AI usingAI;

    public Transform DamagePopupTransform;
    public Transform EnemyPopUpTransform;

    public bool[] FightOptionBool = new bool[2];

    public int[] ConditionSeverity = new int[10];

    public bool bossImmunities;

    public int EnemyID;
    
    public BossList checkList = new BossList { bossesDefeated = new bool[10] };

    public bool StunActive;




    // Start is called before the first frame update
    void Awake()
    {
        expValue *= level;
        AttackDamage *= level;
        IncreaseMaxHealth *= level;
        HealAmount *= level;
        Defense *= level;
        Initiative *= level;

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

        if (bossImmunities)
        {
            SaveSystem.checkIfExists("/bossList.txt");
            SaveSystem.LoadBossList(checkList);
        }


    }

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount, string type)
    {
        amount -= Defense;
        if (amount <= 0)
        {
            amount = 1;
        }
        EnemyHealth.DamageUnit(amount);
        DamagePopup.Create(amount, type, Player.DamagePopupTransform.position);
        
        if (EnemyHealth._currentHealth <= 0)
        {
            Dead();
        }
    }

    public void TurnStart()
    {
        if (ConditionSeverity[1] > 0)
        {
  
            StunActive = true;
            ConditionSeverity[1]--;
        }
        else
        {
            StunActive = false;
            
        }
        if (!StunActive)
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
                    case 0: Attack(); EndTurn(); break;
                    case 1: Heal(); EndTurn(); break;
                }
            }
        }
        else
        {
            EndTurn();
        }


    }

    public void OnMouseDown()
    {
       
        if (Player.isAttackMode)
        {
            Player.targetEnemy = gameObject;
            Player.DamagePopupTransform = this.gameObject.transform.GetChild(0);
            float damageDealt = ((float)Player.DamageToDeal * (float)((float)Random.Range(900, 1301) / 1000f));
            TakeDamage((int)damageDealt, "Damage");

            for (int i = 0; i < ConditionSeverity.Length; i++)
            {
                if(Player.nextHitEffects[i] > ConditionSeverity[i])
                {
                    ConditionSeverity[i] = Player.nextHitEffects[i];
                }
            }

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
        Player.DamagePlayer((int)damageDealt, "Damage");
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
            DamagePopup.Create(HealAmount, "Heal", EnemyPopUpTransform.position);
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
            DamagePopup.Create(HealAmount, "Heal", EnemyPopUpTransform.position);
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
                    DamagePopup.Create(HealAmount, "Heal", EnemyPopUpTransform.position);
                    EnemyLowHPScript.EnemyHealth.HealUnit(HealAmount);
        }

    }

    public void EndTurn()
    {
        ApplyStatusEffects();
        FightManager.NextTurn();
    }

    public void ApplyStatusEffects()
    {
        for(int i = 0; i < ConditionSeverity.Length; i++)
        {
            if (ConditionSeverity[i] > 0)
            {
                StartCoroutine(EffectApplied(i));
                ConditionSeverity[i]--;
            }
        }
    }

    IEnumerator EffectApplied(int ID)
    {
        yield return null;
        switch (ID)
        {
            case 0:
                //Poison
                yield return new WaitForSeconds(0.2f);
                PoisonDMG(ConditionSeverity[ID]);
                yield return new WaitForSeconds(0.2f);
                break;
                case 1:
                //Stun
                yield return new WaitForSeconds(0.2f);
                StunEffect(ConditionSeverity[ID]);
                yield return new WaitForSeconds(0.2f);
                break;

        }
    }

    public void Dead()
    {
        if (EnemyID >= 50)
        {
            checkList.bossesDefeated[EnemyID-50] = true;
        }
        if (bossImmunities)
        {
            SaveSystem.SaveBossList(checkList);

        }


        FightManager.checkForEndFight();

        Initiative = null;
        gameObject.SetActive(false);
    }


    public void PoisonDMG(int Severity)
    {
        if (bossImmunities)
        {
            int dmgToDeal = (int)(EnemyHealth._currentHealth * (Severity * 0.01));

            if (dmgToDeal <= 0)
            {
                dmgToDeal = 1;
            }
            TakeDamage(dmgToDeal, "Poison");
        }
        else
        {
            int dmgToDeal = (int)(EnemyHealth._currentMaxHealth * (Severity * 0.03));

            if (dmgToDeal <= 0)
            {
                dmgToDeal = 1;
            }
            TakeDamage(dmgToDeal, "Poison");
        }

    }

    public void StunEffect(int Severity)
    {
        if (!bossImmunities)
        {
            ConditionSeverity[1] = Severity;
        }
    }



    public int? GetInitiative() { return Initiative; }
    public int GetHealth() { return EnemyHealth._currentHealth; }
}



public class BossList
{
    public bool[] bossesDefeated;
}