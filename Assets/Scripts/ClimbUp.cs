using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ClimbUp : MonoBehaviour
{
    private bool canClimb = false;
    private Rigidbody rb;
    private RigidbodyFirstPersonController cc;
    public Animator anims;
    public Camera parkourCam;
    public Camera regularCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<RigidbodyFirstPersonController>();
        regularCam.depth = 1;
        parkourCam.depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClimb && Input.GetKeyDown(KeyCode.E))
        {
            regularCam.depth = 0;
            parkourCam.depth = 1;
            cc.enabled = false;
            rb.isKinematic = true;
            anims.SetTrigger("Climb");
            StartCoroutine(afterClimb());
            Debug.Log("parkourCam" + parkourCam.depth);
            Debug.Log("regularCam" + regularCam.depth);
        }
    }

    IEnumerator afterClimb()
    {
        yield return new WaitForSeconds(1);
        regularCam.depth = 1;
        parkourCam.depth = 0;
        cc.enabled = true;
        rb.isKinematic = false;
        transform.position = parkourCam.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Climb")
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canClimb = false;
    }
}
