using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    GameObject pl, tc;
    public GameObject mapPrefab, eggVisual, crashVisual;
    public GameObject[] mapSegments;
    bool endTrigger;
    int crashes;
    public float givenIngredients;
    int r;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        
        pl.GetComponent<IngredientHolder>().maxIngredients = 250f;

        Vector3 temp2 = new Vector3(0f, 0f, 0f);
        GameObject arena = Instantiate(mapPrefab, temp2, Quaternion.identity);

        GameObject[] prefabPos = GameObject.FindGameObjectsWithTag("PrefabSpot");

        for(int i = 0; i < prefabPos.Length; i++)
        {
            r = Random.Range(0, mapSegments.Length);

            GameObject segment = Instantiate(mapSegments[r], prefabPos[i].transform.position, Quaternion.identity);
        }

        GameObject egg = Instantiate(eggVisual, pl.GetComponent<PlayerController>().eggSpot.position, Quaternion.identity);
        egg.transform.parent = pl.GetComponent<PlayerController>().eggSpot;

        pl.GetComponent<PlayerController>().crashDetected = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(pl.GetComponent<PlayerController>().crashDetected > crashes)
        {
            crashes = pl.GetComponent<PlayerController>().crashDetected;
            pl.GetComponent<IngredientHolder>().heldIngredients += givenIngredients;
            pl.GetComponent<IngredientHolder>().update = true;

            pl.GetComponent<PlayerController>().canJump = true; //rush state disables jump, re-enables it after end, but when ending early via crash it must be reenabled manually
            
            GameObject cv = Instantiate(crashVisual, pl.transform.position, Quaternion.identity);
        }
        
        if((endTrigger == false) && (tc.GetComponent<TimerController>().time == 1f))
        {
            endTrigger = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator End() //to reset player variables - ingredient spawning and spawned stuff reset themeselves
    {
        yield return new WaitForSeconds(0.95f);
        pl.GetComponent<PlayerController>().canJump = true;
        
    }
}
