using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndStop : MonoBehaviour
{
    public float scale, stopDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == ("Prep Stopper"))
        {
            StartCoroutine(StopDelayTime());
        }
    }

    public IEnumerator StopDelayTime()
    {
        yield return new WaitForSeconds(stopDelay);
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
