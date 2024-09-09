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

    public void addExp(int amount)
    {
        currExp += amount;

        if (currExp >= expToLvlUp)
        {
            LevelUp();

        }
        Debug.Log(currExp);
    }

    public void LevelUp()
    {
        currExp -= expToLvlUp;
        level++;
        expToLvlUp = (int)(expToLvlUp * 1.5f);
        PlayerInEncounter.LevelUp();
    }

}
