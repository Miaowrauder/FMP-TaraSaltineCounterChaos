using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GeneralManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dnd, prepCam;
    public int dishID;
    public int[] minigameID;
    public int[] prepIngredientID;
    public GameObject[] gameController;
    public GameObject pl;
    public float[] gameScore;
    public float gameDuration, breakDuration;
    public bool gameTrigger, breakTrigger, endTrigger;
    public int game;
    public Canvas endCanvas;
    public TMP_Text endScore;
    GameObject tc, dpc;

    int initiatedGame, initiatedBreak;
    
    void Start()
    {
        endCanvas.enabled = false;
        tc = GameObject.Find("In-Game UI");
        dnd = GameObject.Find("Data Holder");
        pl = GameObject.FindWithTag("Player");
        dishID = dnd.GetComponent<DataHolder>().dishID;
        DecodeID();  
    }

    // Update is called once per frame
    void Update()
    {
        if(gameTrigger)
        {
            gameTrigger = false;
            InitiateGame();
        }

        if(breakTrigger)
        {
            breakTrigger = false;
            InitiateBreak();
        }

        if(endTrigger)
        {
            endTrigger = false;
            InitiateEnd();
        }
    }

    void DecodeID()
    {
        if(dishID == 0)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 8; // should be 0, 1, 2 - changed for testing
            minigameID[1] = 8; 
            minigameID[2] = 8; //ouzo halloumi, chs,oil,jui

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; //No visuals, so prep ingredients are set to a basic set to demonstrate system - system works for different ingredients
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }
        else if(dishID == 1)
        {
            minigameID = new int[3];
            gameScore = new float[3];
            
            minigameID[0] = 0; 
            minigameID[1] = 3; 
            minigameID[2] = 4; //pizz, chs, veg, whe

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;

        }
        else if(dishID == 2)
        {
            minigameID = new int[3];
            gameScore = new float[3];
            
            minigameID[0] = 1; 
            minigameID[1] = 5; 
            minigameID[2] = 4; //calamari, oil,squ,whe

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }
        else if(dishID == 3)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 0; 
            minigameID[1] = 6; 
            minigameID[2] = 4; //strawb risotto, chs,fru,whe

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }
        else if(dishID == 4)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 5; 
            minigameID[1] = 3; 
            minigameID[2] = 6; //simmered squid, squ,veg,fru

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }
        else if(dishID == 5)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 7; 
            minigameID[1] = 2; 
            minigameID[2] = 3; //coq au vin, chi, jui, veg

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;

        }
        else if(dishID == 6)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 7; 
            minigameID[1] = 8; 
            minigameID[2] = 4; //chicken laska, chi,sug,whe

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }
        else if(dishID == 7)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 7; 
            minigameID[1] = 5; 
            minigameID[2] = 3; //paella, chi,squ,veg

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }
        else if(dishID == 8)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 1; 
            minigameID[1] = 8; 
            minigameID[2] = 6; //pineapple steak, oil,sug,fru

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }

        else if(dishID == 9)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 2; 
            minigameID[1] = 8; 
            minigameID[2] = 3; //bloody mary soup, jui, sug, veg

            prepIngredientID = new int[3];

            prepIngredientID[0] = 0; 
            prepIngredientID[1] = 1;
            prepIngredientID[2] = 2;
        }

    }

    public void InitiateGame()
    {
        GameObject gc = Instantiate(gameController[(minigameID[game])]);

        GameObject tc = GameObject.Find("In-Game UI");
        tc.GetComponent<TimerController>().inBreakState = false;
        tc.GetComponent<TimerController>().inGameState = true;

        initiatedGame++;
    }

    public void InitiateBreak()
    {

        gameScore[game] = (pl.GetComponent<IngredientHolder>().heldIngredients / pl.GetComponent<IngredientHolder>().maxIngredients); //converts held & max ingredients into a score value from 0 to 1 (easier to use in scaling later, 1 being perfect)
        pl.GetComponent<IngredientHolder>().heldIngredients = 0f;

        pl.GetComponent<IngredientHolder>().ingSlider.maxValue = pl.GetComponent<IngredientHolder>().maxIngredients;
        pl.GetComponent<IngredientHolder>().ingSlider.value = pl.GetComponent<IngredientHolder>().heldIngredients;

        GameObject ingSpawner = GameObject.Find("Ingredient Spawning");
        ingSpawner.GetComponent<IngredientSpawning>().isActive = false;
        

        GameObject[] ingSpots = GameObject.FindGameObjectsWithTag("IngSpot");

        for(int a = 0; a < ingSpots.Length; a++) //destroy all game pieces in arena
        {
            Destroy(ingSpots[a]);
        } 

        GameObject[] windColls = GameObject.FindGameObjectsWithTag("WindCollider");

        for(int a = 0; a < windColls.Length; a++) //destroy all game pieces in arena
        {
            Destroy(windColls[a]);
        } 

        GameObject[] gamePieces = GameObject.FindGameObjectsWithTag("Game Piece");

        for(int a = 0; a < gamePieces.Length; a++) //destroy all ing spots
        {
            Destroy(gamePieces[a]);
        }

        GameObject[] prefabSpots = GameObject.FindGameObjectsWithTag("PrefabSpot");

        for(int a = 0; a < prefabSpots.Length; a++) //destroy all ing spots
        {
            Destroy(prefabSpots[a]);
        }

        

        tc.GetComponent<TimerController>().inBreakState = true;
        tc.GetComponent<TimerController>().inGameState = false; 

        if(game <= minigameID.Length)
        {
            game++;
        }
        
        pl.GetComponent<PlayerController>().canMove = true;
        pl.GetComponent<PlayerController>().canGravity = true;
        
        if(game >= minigameID.Length)
        {
            InitiatePrep();
        }

         initiatedBreak++;

        
        
    }

    public void InitiatePrep()
    {
        tc.GetComponent<TimerController>().inPrepState = true;
        prepCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        pl.GetComponent<PlayerController>().canMove = false;
        pl.GetComponent<PlayerController>().canJump = false;
        pl.GetComponent<PlayerController>().canGravity = false;
    }

    public void InitiateEnd()
    {
        endCanvas.enabled = true;

        dpc = GameObject.FindWithTag("PrepController"); 
        endScore.text = (" " + dpc.GetComponent<DishPrepController>().avgScore + "%");
    }

    public void OnEnd()
    {
        GameObject dh = GameObject.Find("Data Holder");
        dh.GetComponent<DataHolder>().savedScore = dpc.GetComponent<DishPrepController>().avgScore;
        dh.GetComponent<DataHolder>().isReturning = true;
        SceneManager.LoadScene("PreGameScene");
    }

}
