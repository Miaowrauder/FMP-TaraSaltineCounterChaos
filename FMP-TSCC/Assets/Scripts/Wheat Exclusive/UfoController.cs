using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UfoController : MonoBehaviour
{
    public GameObject mountPos;
    public float moveSpeed;
    public GameObject windPrefab;
    public Transform windPos;
    public float energy, maxEnergy;
    Rigidbody rb;
    bool canLoop;
    GameObject wind, cam;
    Transform camTransform;
    Slider mgSlider;
    // Start is called before the first frame update
    void Start()
    {
       rb = this.GetComponent<Rigidbody>(); 
       
       GameObject temp = GameObject.Find("Minigame Slider");
       mgSlider = temp.GetComponent<Slider>();
       mgSlider.maxValue = maxEnergy;


       canLoop = true;
       cam = GameObject.Find("Main Camera");
       camTransform = cam.GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Mouse0) && (energy > 5)))
        {
            wind = Instantiate(windPrefab, windPos.position, Quaternion.identity);
        }

        mgSlider.value = energy;

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
	{
		float moveHorizontal = Input.GetAxis("Horizontal"); 
		float moveVertical = Input.GetAxis("Vertical"); 

		Vector3 moveDir = new Vector3(-moveHorizontal, 0, moveVertical); //simple movement - same as my ktwk movement, bit of a reference
        moveDir = Quaternion.AngleAxis(camTransform.rotation.eulerAngles.y, Vector3.up) * moveDir;

		rb.velocity = moveDir * moveSpeed;
	}

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "WindCollider")
        {
            if(canLoop)
            {
                canLoop = false;
                StartCoroutine(WindCharge());
            }
        }
    }

    public IEnumerator WindCharge()
    {
        yield return new WaitForSeconds(0.2f);
        
        if(energy < maxEnergy) //charge energy if not full, if is full prevents overcapping
        {
            energy += 5;
        }

        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        canLoop = true;
    }
}

