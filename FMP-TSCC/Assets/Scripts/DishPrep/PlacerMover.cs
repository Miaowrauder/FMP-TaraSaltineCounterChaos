using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerMover : MonoBehaviour
{
    public float speed, directionSwapDelay;
    public bool canSwap;
    public int movementTicks;
    public bool firstTick = true;
    // Start is called before the first frame update
    void Start()
    {
        canSwap = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));

        if(canSwap)
        {
            canSwap = false;
            StartCoroutine(SwapDirection());
        }
    }

    public IEnumerator SwapDirection()
    {
        if(!firstTick)
        {
            for(movementTicks = 0; movementTicks < 20; movementTicks++) //split wait into ticks, so wait can be adjusted when click input detected to ensure movement stays centered with plate
            {
                yield return new WaitForSeconds(directionSwapDelay/20f);
                movementTicks++;
            }
            
        }
        else
        {
              
            for(movementTicks = 0; movementTicks < 10; movementTicks++) 
            {
                yield return new WaitForSeconds(directionSwapDelay/20f);
                movementTicks++;
            }

            firstTick = false;

        }
        
        this.transform.Rotate(0, 180, 0);
        canSwap = true;
    }
}
