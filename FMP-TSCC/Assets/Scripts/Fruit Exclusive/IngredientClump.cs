using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientClump : MonoBehaviour
{
    public float health, maxHealth;
    public GameObject ingredientPrefab;
    public GameObject clumpTrigger;
    public Transform[] spawnSpots;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {   
            for(int a = 0; a < spawnSpots.Length; a++)
            {
                GameObject ing = Instantiate(ingredientPrefab, spawnSpots[a].position, Quaternion.identity);
            }

            Destroy(clumpTrigger);
            Destroy(this.gameObject); 
        }
    }
}
