using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCamo : MonoBehaviour
{
    GameObject pl, visual; 
    Transform visTransform;
    RaycastHit hit;
    public LayerMask layerMask;
    int visualGrabDelay;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        visualGrabDelay = 2; //makes sure visual is spawned before grabbing
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(visualGrabDelay == -1)
        {
            //do nothing, prevents the else checking this if last by having it at the front
        }
        else if(visualGrabDelay > 0)
        {
            visualGrabDelay--;
        }
        else if(visualGrabDelay == 0)
        {
            visTransform = this.gameObject.GetComponent<PickupBehaviour>().spawnedVisual.transform.GetChild(0);
            visual = visTransform.gameObject;
            visualGrabDelay = -1;
        }

        this.transform.LookAt(pl.transform.position);

        BackCast();
    }

    void BackCast()
    {
        if(Physics.Raycast(this.transform.position, -this.transform.forward, out hit, 20f, layerMask, QueryTriggerInteraction.Ignore))
        {
            visual.GetComponent<MeshRenderer>().material = hit.collider.gameObject.GetComponent<Renderer>().material;
        }
    }
}
