using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSinker : MonoBehaviour
{
    bool isSinking;
    public Transform topPoint, bottomPoint;
    public GameObject plate;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isSinking)
        {
            plate.transform.position = Vector3.MoveTowards(plate.transform.position, bottomPoint.position, (moveSpeed * Time.deltaTime));
        }
        else
        {
            plate.transform.position = Vector3.MoveTowards(plate.transform.position, topPoint.position, (moveSpeed * Time.deltaTime));
        }
        
        
    }

    private void OnTriggerEnter(Collider coll) 
    {
        if(coll.tag == "Player")
        {
            isSinking = true;
        }
    }

    private void OnTriggerExit(Collider coll) 
    {
        if(coll.tag == "Player")
        {
            isSinking = false;
        }
    }
}
