using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiMover : MonoBehaviour
{
    public Transform movePosition;
    public float moveSpeed;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, movePosition.position, (moveSpeed * Time.deltaTime));
        }
    }
}
