using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class laser : MonoBehaviour
{

    public FirstPersonController firstPersonController;
    public GameObject player;

    private LineRenderer lr;
    [SerializeField]
    private Transform startPoint;
    // Start is called before the first frame update

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
        firstPersonController = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit))
        {
            if(hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if(hit.transform.tag=="Player")
            {
                firstPersonController.isDead = true;
                
            }
        }
        else
        {
            lr.SetPosition(1, -transform.right * 5000);
        }
    }
}
