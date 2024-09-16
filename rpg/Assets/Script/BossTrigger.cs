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

    private void Awake()
    {
        Player = GameObject.FindWithTag("PlayerOW");
        encounterStarter = Player.GetComponent<EncounterStart>();
        GameManagerObject = GameObject.FindWithTag("GameManager");
        GameManager = GameManagerObject.GetComponent<GameManager>();
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
