using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    public int level;
    public int currExp;
    public int expToLvlUp;

    public GameObject PlayerInEncounterObject;
    public PlayerInEncounter PlayerInEncounter;

    private void Awake()
    {
        PlayerInEncounterObject = GameObject.FindWithTag("Player");
        PlayerInEncounter = PlayerInEncounterObject.GetComponent<PlayerInEncounter>();


    }

    private void Start()
    {
        expToLvlUp = (int)(100 * (Mathf.Pow(1.5f, (float)PlayerInEncounter.player.level)));
    }

    public void addExp(int amount)
    {
        PlayerInEncounter.player.exp += amount;

        if (PlayerInEncounter.player.exp >= expToLvlUp)
        {
            LevelUp();

        }
    }

    public void LevelUp()
    {
        PlayerInEncounter.LevelUp();
    }

}
