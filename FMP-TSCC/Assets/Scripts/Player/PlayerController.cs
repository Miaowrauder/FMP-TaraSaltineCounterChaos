using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove, isGrounded, canGravity, canJump;
    public float moveSpeed, jumpHeight, groundDistance, gravity;
    float calcGravity;
    public Transform camTransform, groundCheckTransform;
    public GameObject plHead, cam;
    Rigidbody rb;
    RaycastHit hit;
    bool canCast;
    public int groundDetected, airDetected;
    Vector3 moveDir;
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
        
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            Move();
        }

        if(canGravity)
        {
            Gravity();
        }
        
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

        if(Physics.Raycast(groundCheckTransform.position, Vector3.down, out hit, groundDistance))
        {
            if((hit.collider.tag == ("Environment")))
                {
                    groundDetected++;
                    isGrounded = true;
                    calcGravity = gravity;
                }  
        }
        else
        {
            airDetected++;
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
    

}
