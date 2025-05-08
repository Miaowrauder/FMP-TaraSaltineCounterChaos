using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatBehaviour : MonoBehaviour
{
    public GameObject[] wheatCrop;
    public int detect, undetect;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "WindCollider")
        {
            for(int a = 0; a < wheatCrop.Length; a++)
            {
                wheatCrop[a].transform.LookAt(coll.transform.position);

                Vector3 wheatRotation = wheatCrop[a].transform.localEulerAngles;
                wheatCrop[a].transform.Rotate(-90f, wheatRotation.y, wheatRotation.z);
                wheatCrop[a].transform.localScale = new Vector3(0.2f, 1f, 0.2f);
            }

            detect++;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.tag == "WindCollider")
        {
            for(int a = 0; a < wheatCrop.Length; a++)
            {
                wheatCrop[a].transform.rotation = this.transform.rotation;
                wheatCrop[a].transform.localScale = new Vector3(0.45f, 2f, 0.45f);
            }

            undetect++;
        }
    }
}
