using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseAndSettings : MonoBehaviour
{
    public Canvas pauseMenu;
    public bool isPaused;
    public bool canPause;
    public Transform[] spawnSlot; 
    [Header("Tutorial Prefabs")]
    public GameObject[] cheeseTutorials;
    public GameObject[] oilTutorials;
    public GameObject[] juiceTutorials;
    public GameObject[] vegTutorials;
    public GameObject[] wheatTutorials;
    public GameObject[] squidTutorials;
    public GameObject[] fruitTutorials;
    public GameObject[] chickenTutorials;
    public GameObject[] sugarTutorials;
    GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("Manager");
        pauseMenu.enabled = false;
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(!isPaused && canPause)
            {
                OpenPause();
            }
        }

        if(!canPause)
        {
            pauseMenu.enabled = false; //seperate to pause close because we dont want to lock the cursor since this is used for dishprep
            Time.timeScale = 1f;
        }
    }

    void OpenPause()
    {
        isPaused = true;
        pauseMenu.enabled = true;
        Cursor.lockState = CursorLockMode.None;

        TutorialSpawn();

        Time.timeScale = 0f;
    }

    public void OnClosePause()
    {
        isPaused = false;
        pauseMenu.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    void TutorialSpawn()
    {
        GameObject[] oldIcons = (GameObject.FindGameObjectsWithTag("TempIcon"));

        for(int c = 0; c < oldIcons.Length; c++)
        {
            Destroy(oldIcons[c]);
        }

        int gameID = gm.GetComponent<GeneralManager>().minigameID[gm.GetComponent<GeneralManager>().game];

        if(gameID == 0) //couldve split the prefabs into tutorial slot 1, then used the gameid directly into the array of one for loop for tutorial spawning - but felt this was much easier to assign
        {
            for(int a = 0; a < cheeseTutorials.Length; a++)
            {
                GameObject tut = Instantiate(cheeseTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 1) 
        {
            for(int a = 0; a < oilTutorials.Length; a++)
            {
                GameObject tut = Instantiate(oilTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 2) 
        {
            for(int a = 0; a < juiceTutorials.Length; a++)
            {
                GameObject tut = Instantiate(juiceTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 3) 
        {
            for(int a = 0; a < vegTutorials.Length; a++)
            {
                GameObject tut = Instantiate(vegTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 4) 
        {
            for(int a = 0; a < wheatTutorials.Length; a++)
            {
                GameObject tut = Instantiate(wheatTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 5) 
        {
            for(int a = 0; a < squidTutorials.Length; a++)
            {
                GameObject tut = Instantiate(squidTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 6) 
        {
            for(int a = 0; a < fruitTutorials.Length; a++)
            {
                GameObject tut = Instantiate(fruitTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 7) 
        {
            for(int a = 0; a < chickenTutorials.Length; a++)
            {
                GameObject tut = Instantiate(chickenTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
        else if(gameID == 8) 
        {
            for(int a = 0; a < sugarTutorials.Length; a++)
            {
                GameObject tut = Instantiate(sugarTutorials[a], spawnSlot[a].position, Quaternion.identity);
                tut.transform.parent = spawnSlot[a];
            }
        }
    }

    public void OnBackToMenu()
    {
        GameObject dh = GameObject.Find("Data Holder");
        Destroy(dh);
        SceneManager.LoadScene("PreGameScene");
    }
}
