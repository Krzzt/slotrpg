using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FightManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public PlayerInEncounter Player;
    public int?[] Initiatives;
    public List<GameObject> Characters = new List<GameObject>();
    public List<Enemy> EnemyList = new List<Enemy>();
    public int currTurn = 0;

    public int totalFightExp;

    public bool fightIsOver;

    public GameObject EndPanel;
    public Image PanelVisibility;
    public TMP_Text ExpTextEndOfFight;

    public bool countExpDown = false;
    public bool LoadNewScene = false;


    int framesforCountdown = 0;

    public Biomes BiomeforEnemies;
    public string JsonBiometoDese;

    public int enemiesInFight;
    public Transform[] enemySpawnLocations = new Transform[4];

    List<GameObject> generatedEnemies = new List<GameObject>();
    public GameObject PlayerOWObject;

    public GameObject SaveSystemObject;

    public GameObject UniPlayerObject;
    public ExpSystem ExpSystem;


    // Start is called before the first frame update
    void Awake()
    {
        SaveSystemObject = GameObject.FindWithTag("SaveSystem");
        enemiesInFight = UnityEngine.Random.Range(1, 5);
        PlayerObject = GameObject.FindWithTag("Player"); 
        Player = PlayerObject.GetComponent<PlayerInEncounter>();
        Player.transform.GetChild(1).gameObject.SetActive(true);
        EndPanel = GameObject.FindWithTag("EndPanel");
        PanelVisibility = EndPanel.GetComponent<Image>();
        ExpTextEndOfFight = EndPanel.GetComponentInChildren<TMP_Text>();
        PlayerOWObject = GameObject.FindWithTag("PlayerOW");
        PlayerOWObject.SetActive(false);
        UniPlayerObject = GameObject.FindWithTag("UniPlayer");
        ExpSystem = UniPlayerObject.GetComponent<ExpSystem>();



    }
    private void Start()
    {
        BiomeforEnemies = new Biomes();
        SaveSystem.LoadBiomeforFight(BiomeforEnemies);
        EndPanel.SetActive(false);

        GameObject tempEnemy = BiomeforEnemies.EnemyTypesInBiome[0];
        Enemy tempEnemyScript = tempEnemy.GetComponent<Enemy>();
        if (tempEnemyScript.bossImmunities)
        {
            enemiesInFight = 1;
        }

        for (int i = 0; i < enemiesInFight; i++)
        {
            generatedEnemies.Add(BiomeforEnemies.EnemyTypesInBiome[UnityEngine.Random.Range(0, BiomeforEnemies.EnemyTypesInBiome.Count)]);
            SpawnEnemy(i);
        }


        Characters.Add(PlayerObject);
        for (int i = 0; i < enemiesInFight; i++)
        {
            Characters.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);
       }

        Initiatives = new int?[Characters.Count];
        Initiatives[0] = Player.GetInitiative();
        for (int i = 0; i < Characters.Count - 1; i++)
        {
            GameObject currEnemy = GameObject.FindGameObjectsWithTag("Enemy")[i];
            Enemy currEnemyScript = currEnemy.GetComponent<Enemy>();
            Initiatives[i + 1] = currEnemyScript.GetInitiative();

        }
      
      
        Array.Sort(Initiatives);
        System.Array.Reverse(Initiatives);
        for(int i = 0; i < Initiatives.Length; i++)
        {
            
            for (int a = 0; a < Characters.Count-1; a++)
            {
                GameObject currEnemy = GameObject.FindGameObjectsWithTag("Enemy")[a];
                Enemy currEnemyScript = currEnemy.GetComponent<Enemy>();
                if (Initiatives[i] == currEnemyScript.GetInitiative())
                {
                    GameObject Temp = Characters[i];
                    Characters[i] = currEnemy;
                    Characters[Characters.IndexOf(currEnemy)] = Temp;
                    Temp = null;
                }
                else if (Initiatives[i] == Player.GetInitiative())
                {
                    GameObject Temp = Characters[i];
                    Characters[i] = PlayerObject;
                    Characters[Characters.IndexOf(PlayerObject)] = Temp;
                    Temp = null;
                }
              
            }
            
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            EnemyList.Add(GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<Enemy>());
        }
        EnemyList = EnemyList.OrderByDescending(Enemy => Enemy.Initiative).ToList();
        for (int i = 0; i < EnemyList.Count; i++)
        {
            totalFightExp += EnemyList[i].expValue;

        }
        if (Characters[currTurn] == PlayerObject)
        {
            Player.TurnStart();
        }
        else
        {
            Enemy currEnemyScript = Characters[currTurn].GetComponent<Enemy>();
            currEnemyScript.TurnStart();
        }

    }

    public void NextTurn()
    {
      
        for (int i = 0; i < EnemyList.Count; i++)
        {
            //if ini = null --> skip
            //ini is null if the enemy hits 0HP or lower
            if (EnemyList[i].Initiative == null)
            {
                Characters.Remove(EnemyList[i].gameObject);
                EnemyList.Remove(EnemyList[i]);
            }
        }
        if (Characters.Count <= 1)
        {
            EndFight();
        }
        StartCoroutine(WaitPlease());



  
    }

    IEnumerator WaitPlease()
    {
        if (Characters.Count <= 1)
        {
            EndFight();
        }
        yield return new WaitForSeconds(1);
        currTurn++;
        if (currTurn > Characters.Count - 1)
        {
            currTurn = 0;
        }
        if (Characters[currTurn] == PlayerObject)
        {
            if (Characters.Count <= 1)
            {
                EndFight();
            }
            Player.TurnStart();
        }
        else
        {
            Enemy currEnemyScript = Characters[currTurn].GetComponent<Enemy>();
            currEnemyScript.TurnStart();
        }
    
    }

    public void EndFight()
    {
       
        EndPanel.SetActive(true);
        ExpTextEndOfFight.SetText("Exp: " + totalFightExp);
        fightIsOver = true;
        StartCoroutine(WaitandSetEXP());
       
      
        //SceneManager.LoadScene(0);
    }

    IEnumerator WaitandSetEXP()
    {
        yield return new WaitForSeconds(1);

        countExpDown = true;
     
       
    }
    // Update is called once per frame
    void Update()
    {
        if (countExpDown)
        {
            if((totalFightExp-framesforCountdown) >= 0)
            {
                framesforCountdown++;
                ExpTextEndOfFight.SetText("Exp: " + (totalFightExp - framesforCountdown));
                ExpSystem.addExp(1);
            }
            else
            {
                ExpTextEndOfFight.SetText("Exp: 0");
                StartCoroutine(EndEncounter());
            }
           
           



        }
       
    }
    IEnumerator EndEncounter()
    {
        SaveSystem.SavePlayer(Player.player);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

    public void SpawnEnemy(int index)
    {
        GameObject enemy = Instantiate(generatedEnemies[index], enemySpawnLocations[index].position, Quaternion.identity);
    }

    
}
