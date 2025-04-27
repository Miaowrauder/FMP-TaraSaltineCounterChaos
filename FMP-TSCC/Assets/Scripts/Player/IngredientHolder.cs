using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientHolder : MonoBehaviour
{
    public float maxIngredients, heldIngredients;
    public Slider ingSlider;
    public bool update;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(heldIngredients > maxIngredients)
        {
            heldIngredients = maxIngredients;
        }
        else if(heldIngredients < 0)
        {
            heldIngredients = 0;
        }

        if(update)
        {
            update = false;
            ingSlider.maxValue = maxIngredients;
            ingSlider.value = heldIngredients;
        }
    }
}
