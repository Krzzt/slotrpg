using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTrigger : MonoBehaviour
{
    public GameObject GameManagerObject;
    public GameManager GameManager;
    public Biomes BiomeOfThisTrigger;
    private void Awake()
    {
        GameManagerObject = GameObject.FindWithTag("GameManager");
        GameManager = GameManagerObject.GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerOW")
        {
            GameManager.currentBiome = BiomeOfThisTrigger;
        }
    }

}
