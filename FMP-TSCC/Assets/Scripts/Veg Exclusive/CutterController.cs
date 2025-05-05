using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterController : MonoBehaviour
{
    // Start is called before the first frame update
    public WheelCollider cutterWheel, backCutterWheel, rCutterWheel, rBackCutterWheel, midWheel, midWheel2;
    public float acceleration;
    float currentAcceleration;
    public float brakeForce;
    float currentBrakeForce;
    public float turnAngle;
    float currentTurnAngle;
    public GameObject mountPos;
    Rigidbody rb;
    RaycastHit hit;
    public LayerMask layerMask;
    public bool isGrounded;
    public Transform castPos;
    void Start()
    {
        currentAcceleration = 0f;
        currentBrakeForce = 0f;
        currentTurnAngle = 0f;
        brakeForce = 300f;
        acceleration = 1000f;
        turnAngle = 30f;

        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DownCast();
    }

    void FixedUpdate()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.Space)) //braking
        {
            currentBrakeForce = brakeForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }

        rBackCutterWheel.motorTorque = currentAcceleration;
        backCutterWheel.motorTorque = currentAcceleration;

        midWheel2.motorTorque = currentAcceleration;
        midWheel.motorTorque = currentAcceleration;

        cutterWheel.brakeTorque = currentBrakeForce;
        backCutterWheel.brakeTorque = currentBrakeForce;
        rCutterWheel.brakeTorque = currentBrakeForce;
        rBackCutterWheel.brakeTorque = currentBrakeForce;


        currentTurnAngle = turnAngle * Input.GetAxis("Horizontal");
        cutterWheel.steerAngle = -currentTurnAngle;
        rCutterWheel.steerAngle = -currentTurnAngle;

    }

    public void OnTriggerEnter(Collider coll)
    {
        if((coll.tag == "Ramp") && (isGrounded == true))
        {
            rb.AddForce(Vector3.up * 7000, ForceMode.Impulse);
        }
        if(coll.tag == ("RespawnHazard"))
        {
            GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
            this.transform.position = spawnPoint.transform.position;
        }
    }


    void DownCast()
    {

        if(Physics.Raycast(castPos.position, Vector3.down, out hit, 0.7f, layerMask, QueryTriggerInteraction.Ignore))
        {
            if((hit.collider.tag == ("Environment")))
            {
                isGrounded = true;
            }  
        }
        else
        {
            isGrounded = false;
        }
    }


    

}
