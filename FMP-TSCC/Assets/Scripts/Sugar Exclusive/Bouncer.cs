using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject pl;
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            float height =  pl.GetComponent<PlayerController>().calcGravity; //grab effective gravity

            pl.GetComponent<PlayerController>().calcGravity = pl.GetComponent<PlayerController>().gravity; //set gravity to default for bounce

            pl.GetComponent<Rigidbody>().AddForce(Vector3.up * (height*25), ForceMode.Impulse); //bouncer using grabbed gravity as height
            
        }
    }
}
