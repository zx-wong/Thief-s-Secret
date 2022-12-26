using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class AiAttack : MonoBehaviour
{
    public AiManager aiManager;
    public AiSensor aiSensor;
    public FirstPersonController firstPersonController;

    public GameObject player;

    public float range = 5f;

    // Start is called before the first frame update
    void Start()
    {
        aiManager = GetComponent<AiManager>();
        aiSensor = GetComponent<AiSensor>();

        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonController = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * range;
        Vector3 height = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Debug.DrawRay(height, forward, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(height, forward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //Debug.Log("Shoot Player");
                aiManager.drawGun = true;
            }
        }
    }
}
