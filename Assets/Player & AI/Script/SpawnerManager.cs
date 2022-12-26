using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject ai;

    public int aiNum = 0;

    public GameObject mSpawnPoint;
    public GameObject megan;
    

    // Start is called before the first frame update
    public void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("AiSpawnPoint");
        ai = (GameObject)Resources.Load("AI", typeof(GameObject));
        ai.SetActive(true);
        ai.GetComponent<AiManager>().enabled = true;

        SpawnAi();

        mSpawnPoint = GameObject.Find("MeganSpawnPoint");
        megan = (GameObject)Resources.Load("Megan", typeof(GameObject));
        megan.SetActive(true);
        megan.GetComponent<ResidentController>().enabled = true;

        SpawnMegan();


    }

    // Update is called once per frame
    public void Update()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("Nothing");
        }
    }

    public void SpawnAi()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject AiClone = Instantiate(ai, spawnPoints[i].transform.position, Quaternion.identity) as GameObject;
            AiClone.name = ("AiClone " + (i + 1));
            ai.GetComponent<AiManager>().enabled = true;
            ai.SetActive(true);
        }
    }

    public void SpawnMegan()
    {
        GameObject meganClone = Instantiate(megan, mSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        megan.GetComponent<ResidentController>().enabled = true;
        megan.SetActive(true);
    }
}
