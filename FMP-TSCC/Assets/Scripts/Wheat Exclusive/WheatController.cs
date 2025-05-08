using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WheatController : MonoBehaviour
{
    GameObject ingSpawner, pl, tc, pc;
    public GameObject ingredientPrefab, ingredientPrefab2, ufoPrefab, mapPrefab;
    GameObject ufo;
    bool endTrigger;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pc = GameObject.Find("Player Camera");
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        pl.GetComponent<PlayerController>().canMove = false;
        pl.GetComponent<PlayerController>().canGravity = false;
        
        Vector3 temp = new Vector3(-5f, 8f, 20f);
        GameObject ufo = Instantiate(ufoPrefab, temp, Quaternion.identity);
        pl.GetComponent<PlayerController>().mountPos = ufo.GetComponent<UfoController>().mountPos;
        pl.GetComponent<IngredientHolder>().maxIngredients = 125f;

        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Height = 7f; //sets camera to wider orbit distance
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height = 1.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Height = -4.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = 3.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 8f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = 3.5f;

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 0; //select spawning type
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 0.8f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawnedPrefab = ingredientPrefab2; 
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawning = true; 
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawnWeightOutOfTen = 1; 
        ingSpawner.GetComponent<IngredientSpawning>().setY = -0.5f;
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;

        Vector3 temp2 = new Vector3(-27f, -9.5f, 37.5f);
        GameObject arena = Instantiate(mapPrefab, temp2, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if((endTrigger == false) && (tc.GetComponent<TimerController>().time == 1f))
        {
            endTrigger = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator End() //to reset player variables - ingredient spawning and spawned stuff reset themeselves
    {
        yield return new WaitForSeconds(0.95f);

        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Height = 4.5f; //resets camera to normal orbit distance
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height = 1.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Height = -1.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = 3.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 5.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = 3.5f;

        pl.GetComponent<PlayerController>().canMove = true;
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawning = false;
        pl.GetComponent<PlayerController>().canGravity = true;
        
    }
}
