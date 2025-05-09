using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoundController : MonoBehaviour
{
    public bool isActive, sliderSet;
    bool canCountdown;
    public float poundCooldown, poundMult;
    float currentPoundCooldown;
    Slider mgSlider;
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
            if((currentPoundCooldown <= 0f) && Input.GetKeyDown(KeyCode.LeftShift) && (this.gameObject.GetComponent<PlayerController>().isGrounded == false))
            {
                Pound();
            }

            mgSlider.value = (poundCooldown - currentPoundCooldown);
        }

        if(canCountdown && (poundCooldown > 0))
        {
            StartCoroutine(CountDown());
        }

        if(sliderSet)
        {
            sliderSet = false;
            mgSlider.maxValue = poundCooldown;
        }

    }

    void Pound()
    {
        currentPoundCooldown = poundCooldown;

        this.gameObject.GetComponent<PlayerController>().calcGravity *= poundMult;
    }

    public IEnumerator CountDown()
    {
        canCountdown = false;

        yield return new WaitForSeconds(0.1f);
        
        if(currentPoundCooldown > 0f)
        {
            currentPoundCooldown -= 0.05f;
        }
        
        canCountdown = true;
    }
}

