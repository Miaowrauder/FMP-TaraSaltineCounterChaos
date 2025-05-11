using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour
{
    GameObject ingSpawner, pl, tc;
    public GameObject ingredientPrefab, mapPrefab, evilPrefab, whiskVisual;
    bool canLoop;
    public int minHeatLength, maxHeatLength, minBreakLength, maxBreakLength;
    GameObject[] slimes, ings, cms;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        
        pl.GetComponent<IngredientHolder>().maxIngredients = 125f;

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 3; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 1f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;
        

        Vector3 temp2 = new Vector3(0f, 0f, 0f);
        GameObject arena = Instantiate(mapPrefab, temp2, Quaternion.identity);
        slimes = GameObject.FindGameObjectsWithTag("Slime");
        canLoop = true;
        pl.GetComponent<DodgeController>().isActive = true;
        pl.GetComponent<DodgeController>().sliderSet = true;
        pl.GetComponent<PlayerController>().canWhisk = true;

        GameObject whisk = Instantiate(whiskVisual, pl.GetComponent<PlayerController>().holdSpot.position, Quaternion.identity);
        whisk.transform.parent = pl.GetComponent<PlayerController>().holdSpot;
    }

    // Update is called once per frame
    void Update()
    {
        if(canLoop)
        {
            canLoop = false;
            StartCoroutine(HeatSwap());
        }
    }

    private IEnumerator HeatSwap() 
    {
        ings = GameObject.FindGameObjectsWithTag("Game Piece");
        for(int i = 0; i < ings.Length; i++) //break
        {
            Destroy(ings[i]);
        }


        for(int i = 0; i < slimes.Length; i++) //break
        {
            slimes[i].GetComponent<SlimeController>().triggerCool = true;
        }

        int r = Random.Range(minBreakLength, maxBreakLength + 1);
        yield return new WaitForSeconds(r); 

        for(int i = 0; i < slimes.Length; i++) //heat
        {
            slimes[i].GetComponent<SlimeController>().triggerHeat = true;
        }

        ings = GameObject.FindGameObjectsWithTag("Game Piece");
        cms = GameObject.FindGameObjectsWithTag("Chase Module");
        for(int i = 0; i < ings.Length; i++) //heat evilise ingredients
        {
            cms[i].GetComponent<IngredientChase>().chaseSpeed = 4f; //was using transform.getchild to access the chase modules through ings, but was proving inconsistent with indexing order
            ings[i].GetComponent<PickupBehaviour>().appliedValue = -5f;
            GameObject aura = Instantiate(evilPrefab, ings[i].transform.position, Quaternion.identity);
            aura.transform.parent = ings[i].transform;
        }

        int r2 = Random.Range(minHeatLength, maxHeatLength + 1);

        yield return new WaitForSeconds(r); 
        canLoop = true;
    }
}
