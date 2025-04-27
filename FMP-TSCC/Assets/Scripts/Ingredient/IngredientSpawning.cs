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

        if(spawnType == 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), setY, Random.Range(minZ, maxZ));
            GameObject item = Instantiate(spawnedPrefab, spawnPos, Quaternion.identity);
        }

        isSpawning = false;
    }
}
