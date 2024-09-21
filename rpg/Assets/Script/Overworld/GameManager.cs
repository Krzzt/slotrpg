using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Biomes currentBiome;
    public GameObject PlayerEncounterObject;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerEncounterObject = GameObject.FindWithTag("Player");
        PlayerEncounterObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
