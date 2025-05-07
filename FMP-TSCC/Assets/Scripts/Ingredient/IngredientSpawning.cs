using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawning : MonoBehaviour
{   
    [Header("Co-ords For Standard Spawn")]
    public float minX; 
    public float maxX; 
    public float minZ;
    public float maxZ;
    public float setY;

    [Header("Core Details")]
    public float spawnDelay; 
    public int spawnType;
    public GameObject spawnedPrefab;
    [Header("Dual Spawning")]
    public GameObject dualSpawnedPrefab;
    public bool dualSpawning;
    public int dualSpawnWeightOutOfTen;

    [Header("Activity / Triggers")]
    public bool isActive;
    public bool isSpawning;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && !isSpawning)
        {
            StartCoroutine(SpawnIngredient());
        }
    }

    public IEnumerator SpawnIngredient()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        if(isActive)
        {
            if(spawnType == 0)
            {
                Spawn0();
            }
            else if(spawnType == 1)
            {
                Spawn1();
            }     
        }
        

        isSpawning = false;
    }

    void Spawn0()
    {
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), setY, Random.Range(minZ, maxZ));

        if(dualSpawning)
        {
            int spawn2 = Random.Range(0, 11);

            if(spawn2 > dualSpawnWeightOutOfTen)
            {
                GameObject item = Instantiate(spawnedPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                GameObject item = Instantiate(dualSpawnedPrefab, spawnPos, Quaternion.identity);
            }
        }
        else
        {
            GameObject item = Instantiate(spawnedPrefab, spawnPos, Quaternion.identity);
        }
        
    }

    void Spawn1()
    {
        GameObject[] moonSpots = GameObject.FindGameObjectsWithTag("IngSpot");

        int cho = Random.Range(0, (moonSpots.Length));
        int a = Random.Range(0, 360);
        int b = Random.Range(0, 360);
        int c = Random.Range(0, 360);

        Quaternion randomRot = Quaternion.Euler(a, b, c);

        if(dualSpawning)
        {
            int spawn2 = Random.Range(0, 11);

            if(spawn2 > dualSpawnWeightOutOfTen)
            {
                GameObject item = Instantiate(spawnedPrefab, moonSpots[cho].transform.position, randomRot);
            }
            else
            {
                GameObject item = Instantiate(dualSpawnedPrefab, moonSpots[cho].transform.position, randomRot);
            }
        }
        else
        {
            GameObject item = Instantiate(spawnedPrefab, moonSpots[cho].transform.position, randomRot);
        }

    }
}
