using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishSelectionMenu : MonoBehaviour
{
    [Header("Scroll Bar")]
    public GameObject menuBacking;
    public Slider scrollSlider;

    [Header("Icons & Filtering")]
    public int filteredIngredient; //checked against dish minigame IDs to find dish with
    public int[,] dishID = new int[10,3];
    public GameObject[] dishButtonPrefab; //buttons to spawn in slot
    public Transform[] dishButtonSlot; //slots to spawn buttons in
    // Start is called before the first frame update
    void Start()
    {
        DeclareArrays();
        filteredIngredient = 9;
        IconSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        menuBacking.transform.position = new Vector3(960, scrollSlider.value, 0);
    }

    void IconSpawn()
    {
        GameObject[] oldIcons = (GameObject.FindGameObjectsWithTag("TempIcon"));

        for(int c = 0; c < oldIcons.Length; c++)
        {
            Destroy(oldIcons[c]);
        }

        if(filteredIngredient == 9) //behaviour when not filtering for ingredient
        {
            for(int b = 0; b < dishButtonPrefab.Length; b++) //repeat for every dish button prefab
            {

                GameObject icon = Instantiate(dishButtonPrefab[b], dishButtonSlot[b].transform.position, Quaternion.identity); //spawn button at slots in order
                icon.transform.parent = dishButtonSlot[b];   
            
            }
        }
        else if(filteredIngredient < 9)
        {
            int a = 0;

            for(int b = 0; b < dishButtonPrefab.Length; b++) //repeat for every dish button prefab
            {
                if((dishID[b,0] == filteredIngredient) || (dishID[b,1] == filteredIngredient) || (dishID[b,2] == filteredIngredient) ) //does dish button contain ingredient selected? - could switch to for loop system for 4+ ing dishses
                {
                    GameObject icon = Instantiate(dishButtonPrefab[b], dishButtonSlot[a].transform.position, Quaternion.identity); //spawn button at slots in order, but skip to next dish if ingredient selected is not obtained
                    icon.transform.parent = dishButtonSlot[a];
                    a++; //only moves to next slot if icon is spawned succesfully 
                }
                

            
            }

        }

    }

    void DeclareArrays() //surely theres a more efficient way...
    {
        dishID[0,0] = 0; 
        dishID[0,1] = 1; 
        dishID[0,2] = 2; //ouzo halloumi, chs,oil,jui

        dishID[1,0] = 0; 
        dishID[1,1] = 3; 
        dishID[1,2] = 4; //pizz, chs, veg, whe

        dishID[2,0] = 1; 
        dishID[2,1] = 5; 
        dishID[2,2] = 4; //calamari, oil,squ,whe

        dishID[3,0] = 0; 
        dishID[3,1] = 6; 
        dishID[3,2] = 4; //strawb risotto, chs,fru,whe

        dishID[4,0] = 5; 
        dishID[4,1] = 3; 
        dishID[4,2] = 6; //simmered squid, squ,veg,fru

        dishID[5,0] = 7; 
        dishID[5,1] = 2; 
        dishID[5,2] = 3; //coq au vin, chi, jui, veg

        dishID[6,0] = 7; 
        dishID[6,1] = 8; 
        dishID[6,2] = 4; //chicken laska, chi,sug,whe

        dishID[7,0] = 7; 
        dishID[7,1] = 5; 
        dishID[7,2] = 3; //paella, chi,squ,veg

        dishID[8,0] = 1; 
        dishID[8,1] = 8; 
        dishID[8,2] = 6; //pineapple steak, oil,sug,fru

        dishID[9,0] = 2; 
        dishID[9,1] = 8; 
        dishID[9,2] = 3; //bloody mary soup, jui, sug, veg
    }

    public void OnFilter0() //was gonna do this in one function, and grab an id assigned to the filter buttons but didnt think it was worth the time/effort figuring out how to get the manager canvas to recognise which button triggered the function and read its ID
    {
        if(filteredIngredient != 0)
        {
            filteredIngredient = 0; //if not filtering for it, filter for it
        }
        else
        {
            filteredIngredient = 9; //if is filtering for it, undo filter
        }
        
        IconSpawn();
    }

    public void OnFilter1() 
    {
        if(filteredIngredient != 1)
        {
            filteredIngredient = 1; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

    public void OnFilter2() 
    {
        if(filteredIngredient != 2)
        {
            filteredIngredient = 2; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

    public void OnFilter3() 
    {
        if(filteredIngredient != 3)
        {
            filteredIngredient = 3; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

    public void OnFilter4() 
    {
        if(filteredIngredient != 4)
        {
            filteredIngredient = 4; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

    public void OnFilter5() 
    {
        if(filteredIngredient != 5)
        {
            filteredIngredient = 5; 
        }
        else
        {
            filteredIngredient = 9; 
        }
        
        IconSpawn();
    }

    public void OnFilter6() 
    {
        if(filteredIngredient != 6)
        {
            filteredIngredient = 6; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

    public void OnFilter7() 
    {
        if(filteredIngredient != 7)
        {
            filteredIngredient = 7; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

    public void OnFilter8() 
    {
        if(filteredIngredient != 8)
        {
            filteredIngredient = 8; 
        }
        else
        {
            filteredIngredient = 9; 
        }

        IconSpawn();
    }

}
