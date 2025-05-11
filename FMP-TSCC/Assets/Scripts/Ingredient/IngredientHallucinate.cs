using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHallucinate : MonoBehaviour
{
    GameObject pl, visual; 
    Transform visTransform;
    int visualGrabDelay;
    public Material minHallucinate, midHallucinate, maxHallucinate;
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

            if(pl.GetComponent<PlayerController>().hallucinateLevel <= 3)
            {
                visual.GetComponent<MeshRenderer>().material = minHallucinate;
            }
            else if(pl.GetComponent<PlayerController>().hallucinateLevel <= 6)
            {
                visual.GetComponent<MeshRenderer>().material = midHallucinate;
            }
            else if(pl.GetComponent<PlayerController>().hallucinateLevel <= 99)
            {
                visual.GetComponent<MeshRenderer>().material = maxHallucinate;
            }
        }

    }

}
