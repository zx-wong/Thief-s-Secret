using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameDoor : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smooth = 2f;

    public int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doorOpen)
        {
            //rotate the door
            Quaternion doorRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.localRotation, doorRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion doorRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.localRotation, doorRotation2, smooth * Time.deltaTime);
        }
    }

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
    }
}
