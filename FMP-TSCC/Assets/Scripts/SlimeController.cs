using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    public GameObject heatAura;
    public bool angerState;
    GameObject pl, haz;
    NavMeshAgent navAgent;
    bool canLoop;
    public bool triggerHeat, triggerCool;
    public float repathDelay;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        navAgent = this.GetComponent<NavMeshAgent>();
        canLoop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
       if(!angerState)
       {
            if(canLoop)
            {
                canLoop = false;
                StartCoroutine(RandomPathing());
            }
       }
       else
       {
        
        StopCoroutine(RandomPathing());
        navAgent.destination = pl.transform.position;
       } 

       if(triggerHeat)
       {
        triggerHeat = false;
        angerState = true;
        haz = Instantiate(heatAura, this.transform.position, Quaternion.identity);
        haz.transform.parent = this.gameObject.transform;
        navAgent.speed = 5f;
       }
       else if(triggerCool)
       {
        triggerCool = false;
        angerState = false;
        Destroy(haz);
        navAgent.speed = 2.5f;
       }
    }

    public IEnumerator RandomPathing()
    {
        GameObject[] pathSpots = GameObject.FindGameObjectsWithTag("PrefabSpot");

        int r = Random.Range(0, pathSpots.Length);

        navAgent.destination = pathSpots[r].transform.position;

        yield return new WaitForSeconds(repathDelay);

        canLoop = true;
    }
    
}
