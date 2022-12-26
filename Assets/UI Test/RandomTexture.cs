using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomTexture : MonoBehaviour
{
    Renderer rend;

    public Material[] materials;

    int randNum;

    static public int code;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        RandomCode();
        rend.sharedMaterial = materials[code];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomCode()
    {
        randNum = Random.Range(0, materials.Length);
        //Debug.Log("Material: " + randNum);



        if (randNum == 0)
        {
            code = 0;
           
        }


        else if (randNum == 1)
        {
            code = 1;
           
        }

        else if (randNum == 2)
        {
            code = 2;
            
        }
        
               
        
    }
}
