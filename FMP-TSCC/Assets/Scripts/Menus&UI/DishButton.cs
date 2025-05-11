using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishButton : MonoBehaviour
{
    public int dishID;
    public int[] minigameID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay()
    {
        GameObject menu = GameObject.Find("Dish Selection Canvas/Manager");
        menu.GetComponent<DishSelectionMenu>().selectedDishID = dishID;
        menu.GetComponent<DishSelectionMenu>().selectedMinigameIDs = new int[minigameID.Length]; //sets length of menu saved ids to number of games in this dish, required for preview system
        

        for(int a = 0; a < minigameID.Length; a++)
        {
            menu.GetComponent<DishSelectionMenu>().selectedMinigameIDs[a] = minigameID[a];
        }

        menu.GetComponent<DishSelectionMenu>().triggerScreen2 = true;
    }
}
