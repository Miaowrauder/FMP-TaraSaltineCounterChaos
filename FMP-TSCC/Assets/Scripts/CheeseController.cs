using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheeseController : MonoBehaviour
{
    GameObject ingSpawner, pl, tc, pc;
    public GameObject ingredientPrefab, dualIngredientPrefab, mapPrefab;
    public GameObject[] moonPrefabs;
    bool endTrigger;
    // Start is called before the first frame update
    void Start() //choosing NOT to house all the behaviours in one central controller script - keeps things clean and prevents the instantiated controller causing lag becauses its holding 10 times as much as it needs, this is to illustrate base functions that will be put in all the separate game controllers
    {
        ingSpawner = GameObject.Find("Ingredient Spawning");
        pl = GameObject.FindWithTag("Player");
        tc = GameObject.FindWithTag("Timer");
        pc = GameObject.Find("Player Camera");
        pl.GetComponent<PlayerController>().canMove = false;
        pl.GetComponent<PlayerController>().canGravity = false;
        pl.GetComponent<PlayerController>().canFly = true;
        pl.GetComponent<Rigidbody>().useGravity = false;
    
        pl.GetComponent<IngredientHolder>().maxIngredients = 150f;

        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Height = 7f; //sets camera to wider orbit distance
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height = 1.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Height = -4.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = 3.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 8f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = 3.5f;

        Vector3 temp2 = new Vector3(0f, 0f, 0f);
        GameObject arena = Instantiate(mapPrefab, temp2, Quaternion.identity);

        GameObject[] moonSpot = GameObject.FindGameObjectsWithTag("IngSpot"); //moon empties

        for(int a = 0; a < moonSpot.Length; a++) //spawns one of each moon at random moon spots
        {
            int b = Random.Range(0, moonPrefabs.Length);
            GameObject moon = Instantiate(moonPrefabs[b], moonSpot[a].transform.position, Quaternion.identity);
        }

        ingSpawner.GetComponent<IngredientSpawning>().spawnType = 1; //moon spawning
        ingSpawner.GetComponent<IngredientSpawning>().spawnDelay = 0.8f; //set spawning delay
        ingSpawner.GetComponent<IngredientSpawning>().spawnedPrefab = ingredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawnedPrefab = dualIngredientPrefab; 
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawnWeightOutOfTen = 2;
        ingSpawner.GetComponent<IngredientSpawning>().dualSpawning = true;
        ingSpawner.GetComponent<IngredientSpawning>().isActive = true;
   
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

    private IEnumerator End() //to reset player variables - ingredient spawning and spawned stuff reset themeselves (for the most part)
    {
        yield return new WaitForSeconds(0.95f);

        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Height = 4.5f; //resets camera to normal orbit distance
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height = 1.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Height = -1.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = 3.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 5.5f;
        pc.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = 3.5f;

        ingSpawner.GetComponent<IngredientSpawning>().dualSpawning = false;
        pl.GetComponent<PlayerController>().canMove = true;
        pl.GetComponent<PlayerController>().canGravity = true;
        pl.GetComponent<PlayerController>().canFly = false;
        pl.GetComponent<Rigidbody>().useGravity = true;
        
    }
}
