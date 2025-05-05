using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientMove : MonoBehaviour
{
    public float speed;
    public bool isStop, isDown, isForward, isDecay;
    public GameObject shadowPrefab;
    GameObject shadow;
    public Transform castPos;
    RaycastHit hit;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
       shadow = Instantiate(shadowPrefab, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(isForward)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));

            if(isDecay && (speed > 0f))
            {
                speed -= 0.001f;
            }
        }
        else if(isDown)
        {
            transform.Translate(Vector3.up * (Time.deltaTime * -speed));
            DownCast();
        }
        
    }

    public void OnTriggerEnter(Collider coll)
    {
        if(isStop && (coll.tag == "Environment"))
        {
            speed = 0f;

            GameObject self = this.gameObject;
            self.GetComponent<PickupBehaviour>().appliedValue /= 2f;
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

            Destroy(shadow);
            Destroy(this);
        }
    }

    public void DownCast()
    {
        if(Physics.Raycast(castPos.position, castPos.forward, out hit, 999f, layerMask, QueryTriggerInteraction.Ignore))
        {
            shadow.transform.position = hit.point;
            shadow.transform.parent = this.transform;

            float sScale = (10f - Vector3.Distance(shadow.transform.position, this.transform.position)); //makes the shadow grow as the ingredient gets closer to ground
            sScale /= 10f;
            sScale += 0.2f;
            shadow.transform.localScale = new Vector3(sScale, (sScale/20f), sScale);
        }
    }
}
