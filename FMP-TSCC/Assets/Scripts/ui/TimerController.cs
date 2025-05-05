using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    [Header("Core Behaviour")]
    public float time;
    public bool inGameState;
    public bool inBreakState; 
    public bool inPrepState;
    public bool triggerBlackScreen;
    public TMP_Text timerText;
    
    [Header("State Change Screen")]
    public GameObject BlackScreen;
    GameObject screen;
    public Transform[] screenPos;
    public Canvas inGameCanvas, prepCanvas;
    bool canTime;
    GameObject gm;

   [Header("State Icon")]
    public GameObject[] stateIcon;
    public Transform iconPos;
    GameObject bIcon, gIcon;

    [Header("Prep Screen")]
    public GameObject prepPrefab;
    // Start is called before the first frame update
    void Start()
    {
        prepCanvas.enabled = false;
        inBreakState = true;
        bIcon = Instantiate(stateIcon[1], iconPos.position, Quaternion.identity);
        bIcon.transform.SetParent(inGameCanvas.transform);
        
        gm = GameObject.FindWithTag("Manager");
        time = gm.GetComponent<GeneralManager>().breakDuration;
        canTime = true;

        timerText.text = " " + time;
    }

    // Update is called once per frame
    void Update()
    {
        if((time > 0) && (canTime == true) && (!inPrepState))
        {
            StartCoroutine(Timing());
        }

        if(inPrepState)
        {
        inGameCanvas.enabled = false;
        GameObject uic = Instantiate(prepPrefab, this.transform.position, Quaternion.identity);
        time = 0;
        canTime = false;
        inPrepState = false;
        }
    }

    public IEnumerator Timing()
    {
        if(triggerBlackScreen)
        {
            screen = Instantiate(BlackScreen, screenPos[0].position, Quaternion.identity);
            screen.transform.SetParent(inGameCanvas.transform);
            screen.GetComponent<uiMover>().movePosition = screenPos[1];
            screen.GetComponent<uiMover>().canMove = true;
            triggerBlackScreen = false;
        }

        canTime = false;
        yield return new WaitForSeconds(1f);

        Destroy(screen);
        time -= 1f;
        timerText.text = " " + time;

        if(time == 0f)
        {
            yield return new WaitForSeconds(1f);
            StateSwap();
        }
        canTime = true;

        
    
        
    }

    public void StateSwap()
    {
            triggerBlackScreen = true;

            if(inBreakState)
            {
                time = gm.GetComponent<GeneralManager>().gameDuration;
                gm.GetComponent<GeneralManager>().gameTrigger = true;
                Destroy(bIcon);
                gIcon = Instantiate(stateIcon[0], iconPos.position, Quaternion.identity);
                gIcon.transform.SetParent(inGameCanvas.transform);
            }
            else if(inGameState)
            {
                time = gm.GetComponent<GeneralManager>().breakDuration; //alternate states enabled using gm, so that entering game state in first if statement, doesnt trigger the second one and exit back into break
                gm.GetComponent<GeneralManager>().breakTrigger = true;
                Destroy(gIcon);
                bIcon = Instantiate(stateIcon[1], iconPos.position, Quaternion.identity);
                bIcon.transform.SetParent(inGameCanvas.transform);
            }
        
    }

}
