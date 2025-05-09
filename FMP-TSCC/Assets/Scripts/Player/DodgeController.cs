using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeController : MonoBehaviour
{
    public bool isActive, sliderSet;
    bool canCountdown;
    public float dodgeCooldown, speedMult;
    float currentDodgeCooldown;
    Slider mgSlider;
    bool uppedSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Minigame Slider");
        mgSlider = temp.GetComponent<Slider>();

        canCountdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) 
        {
            mgSlider.value = (dodgeCooldown - currentDodgeCooldown);

            if((currentDodgeCooldown <= 0f) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dodge();
            }
        }

        if(canCountdown && (dodgeCooldown > 0))
        {
            StartCoroutine(CountDown());
        }

        if(sliderSet)
        {
            sliderSet = false;
            mgSlider.maxValue = dodgeCooldown;
        }

    }

    void Dodge()
    {
        currentDodgeCooldown = dodgeCooldown;

        this.gameObject.GetComponent<PlayerController>().moveSpeed *= speedMult;
        uppedSpeed = true;
    }

    public IEnumerator CountDown()
    {
        canCountdown = false;

        yield return new WaitForSeconds(0.05f);
        
        if(uppedSpeed)
        {
            uppedSpeed = false;
            this.gameObject.GetComponent<PlayerController>().moveSpeed /= speedMult;
        }
        
        if(currentDodgeCooldown > 0f)
        {
            currentDodgeCooldown -= 0.05f;
        }
        
        canCountdown = true;
    }
}
