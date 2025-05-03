using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    public bool gameTrigger, breakTrigger;
    private int game;
    GameObject tc;
    void Start()
    {
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
    }

    void DecodeID()
    {
        if(dishID == 0)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 0; 
            minigameID[1] = 1; 
            minigameID[2] = 2; //ouzo halloumi, chs,oil,jui
        }
        else if(dishID == 1)
        {
            minigameID = new int[3];
            gameScore = new float[3];
            
            minigameID[0] = 0; 
            minigameID[1] = 0; 
            minigameID[2] = 0; //temp for testing
           // minigameID[1] = 3; 
           // minigameID[2] = 4; //pizz, chs, veg, whe

        }
        else if(dishID == 2)
        {
            minigameID = new int[3];
            gameScore = new float[3];
            
            minigameID[0] = 1; 
            minigameID[1] = 5; 
            minigameID[2] = 4; //calamari, oil,squ,whe
        }
        else if(dishID == 3)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 0; 
            minigameID[1] = 6; 
            minigameID[2] = 4; //strawb risotto, chs,fru,whe
        }
        else if(dishID == 4)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 5; 
            minigameID[1] = 3; 
            minigameID[2] = 6; //simmered squid, squ,veg,fru
        }
        else if(dishID == 5)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 7; 
            minigameID[1] = 2; 
            minigameID[2] = 3; //coq au vin, chi, jui, veg

        }
        else if(dishID == 6)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 7; 
            minigameID[1] = 8; 
            minigameID[2] = 4; //chicken laska, chi,sug,whe
        }
        else if(dishID == 7)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 7; 
            minigameID[1] = 5; 
            minigameID[2] = 3; //paella, chi,squ,veg
        }
        else if(dishID == 8)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 1; 
            minigameID[1] = 8; 
            minigameID[2] = 6; //pineapple steak, oil,sug,fru
        }
        else if(dishID == 9)
        {
            minigameID = new int[3];
            gameScore = new float[3];

            minigameID[0] = 2; 
            minigameID[1] = 8; 
            minigameID[2] = 3; //bloody mary soup, jui, sug, veg
        }

    }

    public void InitiateGame()
    {
        GameObject gc = Instantiate(gameController[minigameID[game]]);

        GameObject tc = GameObject.Find("In-Game UI");
        tc.GetComponent<TimerController>().inBreakState = false;
        tc.GetComponent<TimerController>().inGameState = true;
    }

    public void InitiateBreak()
    {

        gameScore[game] = (pl.GetComponent<IngredientHolder>().heldIngredients / pl.GetComponent<IngredientHolder>().maxIngredients); //converts held & max ingredients into a score value from 0 to 1 (easier to use in scaling later, 1 being perfect)
        pl.GetComponent<IngredientHolder>().heldIngredients = 0f;

        GameObject[] gamePieces = GameObject.FindGameObjectsWithTag("Game Piece");

        GameObject ingSpawner = GameObject.Find("Ingredient Spawning");
        ingSpawner.GetComponent<IngredientSpawning>().isActive = false;

        for(int a = 0; a < gamePieces.Length; a++) //destroy all game pieces in arena
        {
            Destroy(gamePieces[a]);
            tc.GetComponent<TimerController>().inBreakState = true;
            tc.GetComponent<TimerController>().inGameState = false;
        } 

        if(game < minigameID.Length)
        {
            game++;
        }
        
        
        if(game >= minigameID.Length)
        {
            InitiatePrep();
        }

        
        
    }

    public void InitiatePrep()
    {
        tc.GetComponent<TimerController>().inPrepState = true;
        prepCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        pl.GetComponent<PlayerController>().canMove = false;
        pl.GetComponent<PlayerController>().canJump = false;
        pl.GetComponent<PlayerController>().canGravity = false;
    }

}
