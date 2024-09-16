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

    public positionSave savePos = new positionSave();


    private void Awake()
    {

        SaveSystem.checkIfExists("/savePos.txt");
        SaveSystem.LoadPos(savePos);
        GameManagerObject = GameObject.FindWithTag("GameManager"); ;
        GameManagerScript = GameManagerObject.GetComponent<GameManager>();

        gameObject.transform.position = savePos.position;

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
        savePos.position = gameObject.transform.position;
        SaveSystem.checkIfExists("/savePos.txt");
        SaveSystem.SavePos(savePos);
        Debug.Log("postosave" + savePos.position.ToString());
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


public class positionSave
{
   public Vector3 position;
}
