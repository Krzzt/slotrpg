using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class   EncounterStart : MonoBehaviour
{
    public Rigidbody2D rb;
    private int frames = 0;

    public GameObject GameManagerObject;
    public GameManager GameManagerScript;



    

    private void Awake()
    {
        GameManagerObject = GameObject.FindWithTag("GameManager"); ;
        GameManagerScript = GameManagerObject.GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        frames++;
   
        if (frames % 20 == 0)
        {
            

            if (isMoving())
            {
               if (Random.Range(0, 200) == 0)
                {
                    StartEncounter();
                    
                }
            }
            frames = 0;
        }
   
    }


    bool isMoving()
    {
        if (rb.velocity.magnitude > 0 )
        {
          
            return true;
        }
        else
        {
         
          
            return false; 
        }
    }

    public void StartEncounter()
    {
        SaveSystem.checkIfExists("/saveBiome.txt");
        SaveSystem.SaveBiome(GameManagerScript.currentBiome);
        Debug.Log("WE SAVED IT!");
        StartCoroutine(WaitandLoad());
        SceneManager.LoadSceneAsync("InEncounter");
    }


    IEnumerator WaitandLoad()
    {
        yield return new WaitForSeconds(1);
        //SAVE
        SceneManager.LoadScene("InEncounter");
    }
}
