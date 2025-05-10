using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientChase : MonoBehaviour
{
    public GameObject ingredient;
    public bool isRun;
    GameObject pl, target;
    public float chaseSpeed, radius;

    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        DistanceCheck();
    }

    void DistanceCheck()
    {   
        float distance = Vector3.Distance(pl.transform.position, ingredient.transform.position);

        if(distance < radius)
        {
            ingredient.transform.LookAt(pl.transform.position);

            if(!isRun)
            {
                ingredient.transform.Translate(Vector3.forward * (Time.deltaTime * chaseSpeed));
            }
            else
            {
                ingredient.transform.Translate(Vector3.forward * (Time.deltaTime * -chaseSpeed));
            }
            
        }
    }
}
