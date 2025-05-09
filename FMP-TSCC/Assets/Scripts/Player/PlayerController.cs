using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float activeFlightSpeed;

    [Header("Batting Details")]
    public LayerMask batMask;
    RaycastHit batHit;
    public float hitRadius;
    public GameObject batVisual;
    public GameObject projAlignEmpty;
    [Header("Speed Measurement")]
    public float currentSpeed;
    [Header("Ink Details")]
    public int inkLevel;
    public bool inkCheck;
    public Canvas inkCanvas;
    public Canvas inkCanvas1;
    public Canvas inkCanvas2;
    Canvas ink;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        canJump = true;
        canGravity = true;
        rb = GetComponent<Rigidbody>();
        canCast = true;
        isGrounded = true;
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

        if(canBat && Input.GetKeyDown(KeyCode.Mouse0))
        {
            BatAway();
        }

        if(inkCheck)
        {
            inkCheck = false;
            InkEffect();
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
        
    }

    void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.AddForce(moveDir * (jumpHeight*0.4f), ForceMode.Impulse);
            StartCoroutine(JumpDelay());
            isGrounded = false;
            
        }     
    }

    void DownCast()
    {

        if(Physics.Raycast(groundCheckTransform.position, Vector3.down, out hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            if((hit.collider.tag == ("Environment")) || (hit.collider.tag == ("Game Piece")))
                {
                    isGrounded = true;
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
            calcGravity += (calcGravity * 0.02f);
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == ("RespawnHazard"))
        {
            GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
            this.transform.position = spawnPoint.transform.position;
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
