using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    public bool isVertical, isHorizontal, canUse;
    GameObject pl;
    public float launchHeight, reactivateDelay;
    public Material activeMat, inactiveMat;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        canUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if((coll.tag == "Player") && isVertical && canUse)
        {
            pl.GetComponent<Rigidbody>().AddForce(Vector3.up * launchHeight, ForceMode.Impulse);
            canUse = false;
            this.GetComponent<MeshRenderer>().material = inactiveMat;
            StartCoroutine(Reactivation());
        }
        else if((coll.tag == "Player") && isHorizontal && canUse)
        {
            pl.GetComponent<PlayerController>().inRush = true;
            pl.GetComponent<PlayerController>().canJump = false;
            canUse = false;
            this.GetComponent<MeshRenderer>().material = inactiveMat;
            StartCoroutine(Reactivation());
        }
    }

    public IEnumerator Reactivation()
    {
        yield return new WaitForSeconds(reactivateDelay);
        this.GetComponent<MeshRenderer>().material = activeMat;
        canUse = true;
    }
}
