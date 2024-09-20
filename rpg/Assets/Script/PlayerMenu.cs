using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    public GameObject playerMenu;
    public TMP_Text[] abilityText = new TMP_Text[6];
    public bool menuActive;
    public Player playerStats =  new Player
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
    private void Awake()
    {
        playerMenu.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.LoadPlayer(playerStats);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive)
            {
                menuActive = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                menuActive = true;
                Time.timeScale = 0f;
            }
            playerMenu.SetActive(menuActive);

            abilityText[0].SetText("Level: " + playerStats.level);
            abilityText[1].SetText("Exp: " + playerStats.exp + "/" + playerStats.expToNextLevel);
            abilityText[2].SetText("Damage: " + playerStats.AttackDamage);
            abilityText[3].SetText("Health: " + playerStats.currentHealth + "/" + playerStats.MaxHealth);
            abilityText[4].SetText("Defense: " + playerStats.Defense);
            abilityText[5].SetText("Initiative: " + playerStats.Initiative);
        }   
    }
}
