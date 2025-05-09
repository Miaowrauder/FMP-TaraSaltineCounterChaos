using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHover : MonoBehaviour
{
   
    public float speed, directionSwapDelay;
    public bool canSwap;
    public int swapped;
    // Start is called before the first frame update
    void Start()
    {
        canSwap = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.up * (Time.deltaTime * speed));

        if(canSwap)
        {
            canSwap = false;
            StartCoroutine(SwapDirection());
        }
    }

    public IEnumerator SwapDirection()
    {
        yield return new WaitForSeconds(directionSwapDelay);
        
        this.transform.Rotate(180, 0, 0);
        swapped++;
        canSwap = true;
    }
}
