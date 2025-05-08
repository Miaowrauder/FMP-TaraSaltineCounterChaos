using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    [Header("Core Details")]
    public float appliedValue;
    public float repeatedDelay;
    public int visualID;
    public GameObject[] visualPrefab;
    public bool destroyOnCollect;
    bool isRunning;
    GameObject pl;

    [Header("Pickup Effects")]
    public int pickupBehaviourID;
    public float knockupStrength;
    // Start is called before the first frame update
    void Start()
    {
        GameObject visual = Instantiate(visualPrefab[visualID], this.transform.position, this.transform.rotation);
        visual.transform.parent = this.transform;
        pl = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider coll) 
    {
        if(coll.tag == "Player")
        {
            if(destroyOnCollect)
            {
            pl.GetComponent<IngredientHolder>().heldIngredients += appliedValue;
            pl.GetComponent<IngredientHolder>().update = true;
            PickupEffect();
            Destroy(this.gameObject);
            }

            else if(!destroyOnCollect)
            {
                if(!isRunning)
                {
                    StartCoroutine(RepeatedActivation());
                }
            }
        }
        else if(coll.tag == "UfoBeam")
        {
            pl.GetComponent<IngredientHolder>().heldIngredients += appliedValue;
            pl.GetComponent<IngredientHolder>().update = true;
            Destroy(this.gameObject);
        }
        
    }

    public IEnumerator RepeatedActivation()
    {
        isRunning = true;
        pl.GetComponent<IngredientHolder>().heldIngredients += appliedValue;
        pl.GetComponent<IngredientHolder>().update = true;

        PickupEffect();

        yield return new WaitForSeconds(repeatedDelay);
        isRunning = false;
    }

    void PickupEffect()
    {
        Rigidbody rb = pl.GetComponent<Rigidbody>();
        //0 is no behaviour
        if(pickupBehaviourID == 1) //knockup
        {   
            rb.AddForce(Vector3.up * knockupStrength, ForceMode.Impulse);
        }
        else if(pickupBehaviourID == 2)
        {
            
        }
    }

    
}
