using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public int dishID;
    public bool isReturning;
    public float savedScore;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(isReturning)
        {
            GameObject dscm = GameObject.FindWithTag("SelectionManager");

            if((dscm.GetComponent<DishSelectionMenu>().dishScore[dishID]) < savedScore) //only updates if new score is better
            {
                dscm.GetComponent<DishSelectionMenu>().dishScore[dishID] = savedScore;
            }

            Destroy(this.gameObject);
            
        }
    }
}
