using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrain : MonoBehaviour
{
    public bool canLoop, attach;
    public float energyDrain;
    GameObject ufo;
    Transform movePos;
    // Start is called before the first frame update
    void Start()
    {
        canLoop = true;
        ufo = GameObject.Find("UFO body(Clone)");

        movePos = ufo.GetComponent<UfoController>().windPos.transform;
        attach = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            attach = false;
        }

        if(canLoop)
        {
            canLoop = false;
            StartCoroutine(Drain());
        }

        if(attach)
        {
            this.transform.position = movePos.position;
        }
        else
        {
            this.transform.position = new Vector3(99f, 99f, 99f);
            StartCoroutine(Drain()); //delays the destruction by 1 drain tick to ensure triggerexit goes through
        }
        
    }

    public IEnumerator Drain()
    {
        
        yield return new WaitForSeconds(0.2f);

        if(!attach)
        {
            Destroy(this.gameObject);
        }

        if((ufo.GetComponent<UfoController>().energy) > 1)
        {
            ufo.GetComponent<UfoController>().energy -= energyDrain;
        }
        else
        {
            attach = false; // destroy on energy runout
        }
        
        canLoop = true;
    }
}
