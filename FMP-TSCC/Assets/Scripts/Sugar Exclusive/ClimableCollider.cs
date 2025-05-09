using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimableCollider : MonoBehaviour
{
    GameObject pl;
    public float climbSpeed;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider coll)
    {
        if((coll.tag == "Player") && (Input.GetKey(KeyCode.Space)))
        {
            pl.GetComponent<Rigidbody>().AddForce(Vector3.up * climbSpeed, ForceMode.Impulse);
            pl.GetComponent<PlayerController>().calcGravity = pl.GetComponent<PlayerController>().gravity;
        }
    }
}
