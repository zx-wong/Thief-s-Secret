using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] codePoint;
    [SerializeField] GameObject codePlane;

    int randNum;

    // Start is called before the first frame update
    void Start()
    {
        codePoint = GameObject.FindGameObjectsWithTag("CodePoint");

        codePlane = (GameObject)Resources.Load("CodePlane", typeof(GameObject));

        RandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomPoint()
    {
        randNum = Random.Range(0, codePoint.Length);
        //Debug.Log("Point: " + randNum);

        if(randNum == 0)
        {
            GameObject code = Instantiate(codePlane, codePoint[0].transform.position, Quaternion.identity) as GameObject;
            code.transform.SetParent(codePoint[0].transform);
        }

        else if (randNum == 1)
        {
            GameObject code1 = Instantiate(codePlane, codePoint[1].transform.position, Quaternion.identity) as GameObject;
            code1.transform.SetParent(codePoint[1].transform);
        }

        else if (randNum == 2)
        {
            GameObject code2 = Instantiate(codePlane, codePoint[2].transform.position, Quaternion.identity) as GameObject;
            code2.transform.SetParent(codePoint[2].transform);
        }



        
    }
}
