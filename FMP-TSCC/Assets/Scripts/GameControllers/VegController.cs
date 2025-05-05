using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegController : MonoBehaviour
{
    GameObject ingSpawner, pl;
    public GameObject ingredientPrefab, cutterPrefab, mapPrefab;
    GameObject cutter;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pl = GameObject.FindWithTag("Player");
        pl.GetComponent<PlayerController>().canMove = false;
        pl.GetComponent<PlayerController>().canGravity = false;
        
        Vector3 temp = new Vector3(-5f, 0.6f, 20f);
        GameObject cutter = Instantiate(cutterPrefab, temp, Quaternion.identity);
        pl.GetComponent<PlayerController>().mountPos = cutter.GetComponent<CutterController>().mountPos;
        pl.GetComponent<IngredientHolder>().maxIngredients = 40f;

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 0; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 1f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().setY = 10f;
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;

        Vector3 temp2 = new Vector3(-0.6f, -0.8f, 15.5f);
        GameObject arena = Instantiate(mapPrefab, temp2, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
