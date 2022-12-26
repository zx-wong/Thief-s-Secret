using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutSceen : MonoBehaviour
{
    public GameObject destroyco;
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject cutsceneCam;

    private void Start()
    {
        destroyco = GameObject.Find("DestroyCollider");
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        Player.SetActive(false);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(5);
        Player.SetActive(true);
        cutsceneCam.SetActive(false);
        //Destroy(destroyco);
        
        
    }
}
