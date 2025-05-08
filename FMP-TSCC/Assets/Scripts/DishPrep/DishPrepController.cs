using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DishPrepController : MonoBehaviour
{
    public bool isActive, buttonTrigger, endTrigger;
    GameObject prepUI;
    GameObject panel;
    GameObject panel1;
    public float avgScore;

    [Header("Placer")]
    public int placerState;
    public GameObject placerPrefab;
    GameObject placer; 
    GameObject placerSpawnPos;
    [Header("IDs & Prefabs")]
    public int vesselID;
    public int ingID;
    public GameObject[] vesselPrefab;
    GameObject vesselSpawnPos;
    public GameObject[] ingPrefab;
    GameObject prepCam2;
    CinemachineTrackedDolly prepCam2Dolly;

    bool dollyMode, canDolly;
    Camera panCamera;
    Canvas prepCanvas;
    GameObject gm, pm;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UIdelay());
        prepCam2 = (GameObject.Find("Panning Prep Cam"));
        prepUI = GameObject.Find("Dish Prep UI");
        panel = GameObject.Find("LeftPrepPanel");
        panel1 = GameObject.Find("RightPrepPanel");
        placerSpawnPos = GameObject.Find("PlacerPos");
        vesselSpawnPos = GameObject.Find("VesselPos");
        prepCanvas = prepUI.GetComponent<Canvas>();

        gm = GameObject.FindWithTag("Manager");
        pm = GameObject.Find("Pause Manager");
        pm.GetComponent<PauseAndSettings>().canPause = false;
        vesselID = gm.GetComponent<GeneralManager>().dishID;
        Instantiate(vesselPrefab[vesselID], vesselSpawnPos.transform.position, Quaternion.identity); //vessel locked to 0 for demo - visual elements like bowl for soup, base for pizza etc, essentially the extra plating for the dish
    

        for(int b = 0; b < (gm.GetComponent<GeneralManager>().gameScore.Length); b++) //finds mean of all scores
        {
            avgScore += gm.GetComponent<GeneralManager>().gameScore[b];
        }

        avgScore /= gm.GetComponent<GeneralManager>().gameScore.Length;
        avgScore *= 100f;
        avgScore = Mathf.Round(avgScore);

    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Mouse0)) && isActive && (placer != null))
        {

            if(placerState == 1) //checking state in inverse order, so the change in state doesnt immediately trigger the next state functions
            {
                SpawnIngredient();
            }
            else if((placerState == 0) && (placer.GetComponent<PlacerMover>().firstTick == false))
            {
                placer.transform.Rotate(0, 90, 0);
                placer.GetComponent<PlacerMover>().movementTicks = 10;
                placerState++;
            }
            
        }

        if(buttonTrigger)
        {
            buttonTrigger = false;
            ButtonTriggered();
        }

        if(endTrigger)
        {
            endTrigger = false;
            GameEnd();
        }

        if(dollyMode && canDolly)
        {
            canDolly = false;
            StartCoroutine(UIdelay());
        }

    }

    public IEnumerator UIdelay()
    {

        if(!isActive)
        {
            yield return new WaitForSeconds(2f);
            isActive = true;
            prepCanvas.enabled = true;
            panel.GetComponent<uiMover>().canMove = true;
            panel1.GetComponent<uiMover>().canMove = true;

            Cursor.lockState = CursorLockMode.None;
        }

        if(dollyMode)//reusing the ienumerator to move the dolly cart lol
        {
            prepCam2Dolly.m_PathPosition += 0.02f; 
            yield return new WaitForSeconds(0.05f);
            canDolly = true;

            if(prepCam2Dolly.m_PathPosition > 2.2f)
            {
                dollyMode = false;
                EndScreen();
            }
        }

        
            
    }

    public void ButtonTriggered()
    {
        Cursor.lockState = CursorLockMode.Locked;
        placer = Instantiate(placerPrefab, placerSpawnPos.transform.position, Quaternion.identity);
        placerState = 0;
    }
    public void SpawnIngredient()
    {
        GameObject prepIng = Instantiate(ingPrefab[ingID], placer.transform.position, Quaternion.identity);
        prepIng.GetComponent<ScaleAndStop>().scale = ((avgScore/100f) + 0.3f); //this line would, ideally, grab the scale based on the correct game corresponding to the spawned ingredient - however without making all the custom ingredients (which I'm not doing, as its visual and not necessary to demonstrate the system) that can't be done so I'm using average score to determine scale in the demo
        Destroy(placer);
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameEnd()
    {
        prepCanvas.enabled = false;
        prepCam2.GetComponent<CinemachineVirtualCamera>().Priority = 12;
        prepCam2Dolly = (prepCam2.GetComponent<CinemachineVirtualCamera>()).GetCinemachineComponent<CinemachineTrackedDolly>();

        dollyMode = true;
        canDolly = true;
        
    }

    public void EndScreen()
    {
        gm.GetComponent<GeneralManager>().endTrigger = true;
    }

}
