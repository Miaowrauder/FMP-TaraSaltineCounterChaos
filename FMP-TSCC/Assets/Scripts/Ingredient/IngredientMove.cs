using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientMove : MonoBehaviour
{
    public float speed, homeSpeed;
    public bool isStop, isDown, isForward, isDecay, isHome, isExplodeOnStop;
    public GameObject shadowPrefab, explodePrefab;
    GameObject shadow, pl;
    public Transform castPos;
    RaycastHit hit;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        if(isDown)
        {
            shadow = Instantiate(shadowPrefab, this.transform.position, Quaternion.identity);
        }
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
        if(isHome)
        {
            Vector3 relativePos = pl.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * homeSpeed);
        }
        
    }

    public void OnTriggerEnter(Collider coll)
    {
        if(isStop && (coll.tag == "Environment") && !isExplodeOnStop)
        {
            speed = 0f;

            GameObject self = this.gameObject;
            self.GetComponent<PickupBehaviour>().appliedValue /= 2f;
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

            Destroy(shadow);
            Destroy(this);
        }
        else if(isStop && (coll.tag == "Environment") && isExplodeOnStop)
        {
            GameObject explosion = Instantiate(explodePrefab, this.transform.position, Quaternion.identity);
            Destroy(shadow);
            Destroy(this.gameObject);
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
