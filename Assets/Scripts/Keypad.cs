using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    private string currentPassword;
    public string input;
    public bool onTrigger = false;
    public bool doorOpen = false;
    public bool keypadScreen = false;
    public Transform doorHinge;
    public float doorOpenAngle = -90f;
    public float smooth = 2f;
    public GameObject clearPassword;


    private void Start()
    {
        clearPassword = GameObject.Find("Password");
    }

    // Update is called once per frame
    void Update()
    {
        if(input==currentPassword)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            doorOpen = true;
            clearPassword.GetComponent<Text>().color = Color.green;
        }
        if(doorOpen)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0f, doorOpenAngle, 0f), Time.deltaTime * smooth);
            doorHinge.rotation = newRot;

        }

        switch (RandomTexture.code)
        {
            case 0:

                currentPassword = "4585";
                break;

            case 1:

                currentPassword = "2617";
                break;

            case 2:

                currentPassword = "6726";
                break;

            default:
                break;

        }



    }

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
    }

    void OnGUI()
    {
        GUI.skin.box.fontSize = 40;
        GUI.skin.button.fontSize = 40;

        if (!doorOpen)
        {
            Event e = Event.current;

            if (onTrigger)
            {
                GUI.Box(new Rect(640, 270, 600, 60), "Press E to enter keypad");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    keypadScreen = true;
                    onTrigger = false;
                    
                }
            }
            if (keypadScreen)
            {
                GUI.Box(new Rect(640, 1000, 600, 60), "Use keyboard to enter password");
                GUI.Box(new Rect(640, 270, 640, 720), "");
                GUI.Box(new Rect(645, 270, 635, 100), input);

                if (GUI.Button(new Rect(645, 370, 200, 200), "1") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha1) 
                {
                    input = input + "1";
                }
                if (GUI.Button(new Rect(645, 570, 200, 200), "4") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha4)
                {
                    input = input + "4";
                }
                if (GUI.Button(new Rect(645, 770, 200, 200), "7") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha7)
                {
                    input = input + "7";
                }
                if (GUI.Button(new Rect(855, 370, 200, 200), "2") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha2)
                {
                    input = input + "2";
                }
                if (GUI.Button(new Rect(855, 570, 200, 200), "5") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha5)
                {
                    input = input + "5";
                }
                if (GUI.Button(new Rect(855, 770, 200, 200), "8") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha8)
                {
                    input = input + "8";
                }
                if (GUI.Button(new Rect(1060, 370, 200, 200), "3") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha3)
                {
                    input = input + "3";
                }
                if (GUI.Button(new Rect(1060, 570, 200, 200), "6") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha6)
                {
                    input = input + "6";
                }
                if (GUI.Button(new Rect(1060, 770, 200, 200), "9") || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Alpha9)
                {
                    input = input + "9";
                }

            }
        }
    }

}
