using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public bool canMove, isGrounded, canGravity, canJump, canFly, canBat;
    public float moveSpeed, jumpHeight, groundDistance, gravity;
    public float calcGravity;
    public Transform camTransform, groundCheckTransform;
    public GameObject plHead, cam;
    Rigidbody rb;
    RaycastHit hit;
    bool canCast;
    Vector3 moveDir;
    public LayerMask layerMask;
    public GameObject mountPos;
    [Header("Flight Details")]
    public float flightSpeed;
    public float activeFlightSpeed;

    [Header("Batting Details")]
    public LayerMask batMask;
    RaycastHit batHit;
    public float hitRadius;
    public GameObject batVisual;
    public GameObject projAlignEmpty;
    [Header("Speed Measurement / Chicken")]
    public float currentSpeed;
    public float lastSpeed;
    public float speedDifference;
    int speedTick;
    public bool inRush;
    public int crashDetected;
    [Header("Ink Details")]
    public int inkLevel;
    public bool inkCheck;
    public Canvas inkCanvas;
    public Canvas inkCanvas1;
    public Canvas inkCanvas2;
    Canvas ink;
    [Header("Juice Details")]
    public bool hallucinateCheck;
    public int hallucinateLevel;
    public GameObject cineCamera;
    public GameObject ingSpawner;
    [Header("Whisk Details")]
    public float whiskRadius;
    public GameObject whiskVisual;
    public float ingsGained;
    public bool canWhisk;
    [Header("Held Objects")]
    public Transform eggSpot;
    public Transform eggSpotHeld;
    public Transform eggSpotBelow;
    public Transform holdSpot;
    
    GameObject tui;

    GameObject pm;
    // Start is called before the first frame update
    void Start()
    {
        cineCamera = GameObject.Find("Player Camera");
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pm = GameObject.Find("Pause Manager");
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        canJump = true;
        canGravity = true;
        rb = GetComponent<Rigidbody>();
        canCast = true;
        isGrounded = true;
        speedTick = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(canCast)
        {
            DownCast();
        }
        
        if(canJump && (Input.GetButtonDown("Jump")))
        {
            Jump();
        }

        if(!canMove && (mountPos != null))
        {
            this.transform.position = mountPos.transform.position;
        }

        if(canFly)
        {
            if(Input.GetKey(KeyCode.W))
            {
                activeFlightSpeed = flightSpeed;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                activeFlightSpeed = -flightSpeed;
            }
            else
            {
                activeFlightSpeed = 0f;
            }
        }

        if(canBat && Input.GetKeyDown(KeyCode.Mouse0) && (pm.GetComponent<PauseAndSettings>().isPaused == false))
        {
            BatAway();
        }

        if(canWhisk && Input.GetKeyDown(KeyCode.Mouse0) && (pm.GetComponent<PauseAndSettings>().isPaused == false))
        {
            Whisk();
        }

        if(inkCheck)
        {
            inkCheck = false;
            InkEffect();
        }

        if(hallucinateCheck)
        {
            hallucinateCheck = false;
            HallucinateEffect();
        }

        
        
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            Move();
        }
        if(canFly);
        {
            Fly();
        }

        if(canGravity)
        {
            Gravity();
        }

        currentSpeed = Vector3.Magnitude(rb.velocity);

        speedTick--;

        if(speedTick == 0)
        {
            SpeedDelay();
        }

        speedDifference = lastSpeed - currentSpeed;

        if((speedDifference > 10f) && isGrounded)
        {
            crashDetected++;
            inRush = false;
        }
        
    }

    void SpeedDelay()
    {
        lastSpeed = currentSpeed;
        speedTick = 6;
    }

    void Whisk()
    {
        Collider[] hitSlimes = Physics.OverlapSphere(this.transform.position, whiskRadius);

        GameObject whisk = Instantiate(whiskVisual, this.transform.position, Quaternion.identity);

        for(int i = 0; i < hitSlimes.Length; i++)
        {
            if(hitSlimes[i].gameObject.tag == "Slime")
            {
                this.gameObject.GetComponent<IngredientHolder>().heldIngredients += ingsGained;  
                this.gameObject.GetComponent<IngredientHolder>().update = true;
            }
            
        }
    }
    void HallucinateEffect()
    {
        if(hallucinateLevel == 0)
        {
            cineCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 50;
        }
        else if(hallucinateLevel > 0)
        {
            cineCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = (50 + (hallucinateLevel * 5));

            if(hallucinateLevel > 9)
            {
                ingSpawner.GetComponent<IngredientSpawning>().dualSpawnWeightOutOfTen = 5;
            }
            else if(hallucinateLevel > 6)
            {
                ingSpawner.GetComponent<IngredientSpawning>().dualSpawnWeightOutOfTen = 4;
            }
            else if(hallucinateLevel > 3)
            {
                ingSpawner.GetComponent<IngredientSpawning>().dualSpawnWeightOutOfTen = 3;
            }
        }

    }
    void InkEffect() //layers semi-transparent canvases to progressively add inkiness
    {
        if(inkLevel == 1)
        {
           ink = Instantiate(inkCanvas, this.transform.position, Quaternion.identity); //spawn spot doesnt matter since its ui
        }
        if(inkLevel == 2)
        {
            ink = Instantiate(inkCanvas, this.transform.position, Quaternion.identity); //spawn spot doesnt matter since its ui
        }
        if(inkLevel == 3)
        {
            ink = Instantiate(inkCanvas1, this.transform.position, Quaternion.identity); //spawn spot doesnt matter since its ui
        }
        if(inkLevel == 4)
        {
            ink = Instantiate(inkCanvas1, this.transform.position, Quaternion.identity); //spawn spot doesnt matter since its ui
        }
        if(inkLevel == 5)
        {
            ink = Instantiate(inkCanvas2, this.transform.position, Quaternion.identity); //spawn spot doesnt matter since its ui
        }
        if(inkLevel == 6)
        {
            ink = Instantiate(inkCanvas2, this.transform.position, Quaternion.identity); //spawn spot doesnt matter since its ui
        }
    }

    void Fly()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 camForward = new Vector3(cam.transform.forward.x, cam.transform.forward.y, cam.transform.forward.z); //gets the cameras facing transform as a vector3

        Vector3 camRotation = cam.transform.localEulerAngles; //gets the cameras rotation as a vector 3 but takes into account some parenty stuff
        plHead.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, plHead.transform.localEulerAngles.z); //set head to show movement direction

        moveDir = camForward.normalized; //set movement direction as wherever the camera points to

        rb.AddForce(moveDir * activeFlightSpeed, ForceMode.Impulse); //add ya force

    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDir = new Vector3(-horizontalInput, 0, verticalInput);
    
        Vector3 camRotation = cam.transform.localEulerAngles; //grab camera rotation
        plHead.transform.rotation = Quaternion.Euler(plHead.transform.localEulerAngles.x, camRotation.y, plHead.transform.localEulerAngles.z); //apply cam rotation to head

        moveDir = Quaternion.AngleAxis(camTransform.rotation.eulerAngles.y, Vector3.up) * moveDir;

        if(isGrounded)
        {
            rb.AddForce(moveDir * moveSpeed, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(moveDir * (moveSpeed/2), ForceMode.Impulse);
        }

        if(inRush) //only forward momentum
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir = Quaternion.AngleAxis(camTransform.rotation.eulerAngles.y, Vector3.up) * moveDir;
            rb.AddForce(moveDir * (moveSpeed*1.5f), ForceMode.Impulse);
        }
        
    }

    void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.AddForce(moveDir * (jumpHeight*0.4f), ForceMode.Impulse);
            StartCoroutine(JumpDelay());
            isGrounded = false;
            eggSpot.position = eggSpotBelow.position;
            
        }     
    }

    void DownCast()
    {

        if(Physics.Raycast(groundCheckTransform.position, Vector3.down, out hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            if((hit.collider.tag == ("Environment")) || (hit.collider.tag == ("Game Piece")))
                {
                    isGrounded = true;
                    eggSpot.position = eggSpotHeld.position;
                    calcGravity = gravity;
                }  
        }
        else
        {
            isGrounded = false;
        }
    }

    public IEnumerator JumpDelay()
    {   
        canCast = false;
        yield return new WaitForSeconds(0.1f);
        canCast = true;
    }

    void Gravity()
    {
        if(isGrounded == false)
        {
            rb.AddForce(Vector3.down * calcGravity, ForceMode.Impulse);

            if(calcGravity < 30f)
            {
                calcGravity += (calcGravity * 0.02f);
            }
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == ("RespawnHazard"))
        {
            GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
            this.transform.position = spawnPoint.transform.position;

            tui = GameObject.Find("Tara UI"); //concerned reaction
            tui.GetComponent<TaraUiController>().faceID = 2;
            tui.GetComponent<TaraUiController>().textID = 2;
            tui.GetComponent<TaraUiController>().trigger = true;
        
        }
    }

    void BatAway()
    {
        if(Physics.Raycast(camTransform.position, camTransform.forward, out batHit, 99f, batMask)) //magnetises position towards clump point
        {
            if((batHit.collider.gameObject.CompareTag("Clump")))
            {
                projAlignEmpty.transform.position = batHit.collider.transform.position;
            }
        }
        else //true aligned position
        {
           projAlignEmpty.transform.position = camTransform.position + camTransform.forward * 99f;
        }

        Collider[] battedIngs = Physics.OverlapSphere(this.transform.position, hitRadius);

        GameObject swing = Instantiate(batVisual, this.transform.position, Quaternion.identity);

        for(int l = 0; l < battedIngs.Length; l++)
        {
            if(battedIngs[l].gameObject.tag == "Game Piece") //prevents moving of self or environment
            {
                battedIngs[l].gameObject.transform.LookAt(projAlignEmpty.transform.position);
                battedIngs[l].gameObject.GetComponent<PickupBehaviour>().beenDeflected = true;
                battedIngs[l].gameObject.GetComponent<IngredientMove>().isHome = false;
            }
        }

    }


    

}
