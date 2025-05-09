using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidController : MonoBehaviour
{
    GameObject ingSpawner, pl, tc;
    public GameObject ingredientPrefab, ingredientPrefab2, mapPrefab;
    bool endTrigger;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        pl.GetComponent<IngredientHolder>().maxIngredients = 90f;

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 0; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 0.5f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab2; 
        ingSpawner.GetComponent<IngredientSpawning>().setY = 15f;
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;

        Vector3 temp2 = new Vector3(0f, 0f, 0f);
        GameObject arena = Instantiate(mapPrefab, temp2, Quaternion.identity);

        GameObject[] ingSpots = GameObject.FindGameObjectsWithTag("IngSpot");
        
        for(int a = 0; a < ingSpots.Length; a++)
        {
            GameObject ing = Instantiate(ingredientPrefab, ingSpots[a].transform.position, Quaternion.identity); //spawns all on start, collection minigame
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((endTrigger == false) && (tc.GetComponent<TimerController>().time == 1f))
        {
            endTrigger = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator End() //to reset player variables - ingredient spawning and spawned stuff reset themeselves
    {
        yield return new WaitForSeconds(0.95f);
        pl.GetComponent<PlayerController>().inkLevel = 0;
        
    }
}

