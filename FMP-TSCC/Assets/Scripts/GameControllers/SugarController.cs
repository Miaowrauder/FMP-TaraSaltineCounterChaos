using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarController : MonoBehaviour
{
    GameObject ingSpawner, pl, tc;
    public GameObject ingredientPrefab, mapPrefab;
    public GameObject[] smallerPrefabs;
    bool endTrigger;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        
        pl.GetComponent<IngredientHolder>().maxIngredients = 40f;
        pl.GetComponent<PoundController>().isActive = true;
        pl.GetComponent<PoundController>().sliderSet = true;

        Vector3 temp = new Vector3(-2f, 0f, -1f);
        GameObject arena = Instantiate(mapPrefab, temp, Quaternion.identity);

        GameObject[] prefabPos = GameObject.FindGameObjectsWithTag("PrefabSpot"); //moon empties

        for(int a = 0; a < prefabPos.Length; a++) //spawns random bouncers & pillars at the assigned spots
        {
            int b = Random.Range(0, smallerPrefabs.Length);
            GameObject moon = Instantiate(smallerPrefabs[b], prefabPos[a].transform.position, Quaternion.identity);
        }

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 3; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 1.75f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
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
        pl.GetComponent<PoundController>().isActive = false;
    }
}
