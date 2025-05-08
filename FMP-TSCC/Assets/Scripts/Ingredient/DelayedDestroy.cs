using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    public float lifeSpan;
    bool isDestroying;
    // Start is called before the first frame update
    void Start()
    {
        isDestroying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDestroying)
        {
            isDestroying = false;
            StartCoroutine(DestroySelf());
        }
    }

    public IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifeSpan);
        this.transform.position = new Vector3(99f, 99f, 99f);
        yield return new WaitForSeconds(lifeSpan/100f);
        Destroy(this.gameObject);
    }
}