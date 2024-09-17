using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject Player;
    public EncounterStart encounterStarter;
    public GameObject GameManagerObject;
    public GameManager GameManager;

    public Biomes BossBiome;

    public BossList checkIfLoad = new BossList();

    public int bossNumber;

    private void Awake()
    {
        Player = GameObject.FindWithTag("PlayerOW");
        encounterStarter = Player.GetComponent<EncounterStart>();
        GameManagerObject = GameObject.FindWithTag("GameManager");
        GameManager = GameManagerObject.GetComponent<GameManager>();

        SaveSystem.checkIfExists("/bossList.txt");
        SaveSystem.LoadBossList(checkIfLoad);

        if (checkIfLoad.bossesDefeated[bossNumber])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerOW")
        {
            GameManager.currentBiome = BossBiome;
            encounterStarter.StartEncounter();
        }
    }

}
