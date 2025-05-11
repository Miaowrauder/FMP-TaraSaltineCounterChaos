using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGameController : MonoBehaviour
{
    GameObject ingSpawner;
    public GameObject ingredientPrefab;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 0; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 1; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
