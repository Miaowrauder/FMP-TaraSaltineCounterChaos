using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrepButton : MonoBehaviour
{
    public int buttonID; //corresponds to the array slot of the prep ingredient, as well as the buttons positions on the screen;
    GameObject dpc, gm;
    public TMP_Text thisText;
    string buttonString;
    int ing;
    int limitChecks = 5;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");

        
    }

    // Update is called once per frame
    void Update()
    {
        if(limitChecks > 0)
        {
            if(buttonID >= gm.GetComponent<GeneralManager>().prepIngredientID.Length) //destroy uneeded buttons, in update to delay it so it acts after gm sets array length
            {
                Destroy(this.gameObject);
            }
            else
            {
                ButtonVisual();
            }
            limitChecks --;
        }
        
    }

    public void OnPress()
    {
        dpc = GameObject.FindWithTag("PrepController");
        dpc.GetComponent<DishPrepController>().ingID = gm.GetComponent<GeneralManager>().prepIngredientID[buttonID]; //sets the id of ingredient to spawn based on the saved prep id corresponding to the buttons place in the array 
        dpc.GetComponent<DishPrepController>().buttonTrigger = true;
    }

    public void ButtonVisual() //would be replaced with visuals over text - sets button text to show which ingredient piece is being spawned
    {
        ing = gm.GetComponent<GeneralManager>().prepIngredientID[buttonID];

        if(ing == 0)
        {
            buttonString = "Demo Ingredient 0 (Slab)";
        }
        if(ing == 1)
        {
            buttonString = "Demo Ingredient 1 (Cylinder)";
        }
        if(ing == 2)
        {
            buttonString = "Demo Ingredient 2 (Cube)";
        }
        if(ing == 3)
        {
            buttonString = "This shouldn't be visible";
        }
        if(ing == 4)
        {
            buttonString = "This shouldn't be visible";
        }
        if(ing == 5)
        {
            buttonString = "This shouldn't be visible";
        }
        

        //etc...

        thisText.text = buttonString;
        
    }
}
