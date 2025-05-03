using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishPrepController : MonoBehaviour
{
    public bool isActive;
    public GameObject prepUI, panel, panel1;
    Canvas prepCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UIdelay());
        prepUI = GameObject.Find("Dish Prep UI");
        panel = GameObject.Find("LeftPrepPanel");
        panel1 = GameObject.Find("RightPrepPanel");
        prepCanvas = prepUI.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator UIdelay()
    {
        yield return new WaitForSeconds(2f);
        isActive = true;
        prepCanvas.enabled = true;
        panel1.GetComponent<uiMover>().canMove = true;
        panel.GetComponent<uiMover>().canMove = true;
        
    }
}
