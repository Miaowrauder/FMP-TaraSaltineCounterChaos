using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    GameObject ingSpawner, pl, tc;
    public GameObject ingredientPrefab, ingredientPrefab2, clumpPrefab;
    bool endTrigger;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        pl.GetComponent<PlayerController>().canBat = true;
        
        pl.GetComponent<IngredientHolder>().maxIngredients = 80f;
        pl.GetComponent<DodgeController>().isActive = true;
        pl.GetComponent<DodgeController>().sliderSet = true;

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 2; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 0.5f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawnedPrefab = ingredientPrefab2;
        ingSpawner.GetComponent<IngredientSpawning>().bonusSpawnedPrefab = clumpPrefab;
        ingSpawner.GetComponent<IngredientSpawning>().spawnsToTriggerBonus = 10;    
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawning = true;
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawnWeightOutOfTen = 2;
        ingSpawner.GetComponent<IngredientSpawning>().setY = -1f;
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;

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
        pl.GetComponent<PlayerController>().canBat = false;
        ingSpawner.GetComponent<IngredientSpawning>().spawnsToTriggerBonus = 9999;    
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawning = false;
        pl.GetComponent<DodgeController>().isActive = false;
        

        
    }
}

