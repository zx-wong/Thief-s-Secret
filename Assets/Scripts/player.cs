using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] float lookspeed = 10f;
    Vector3 lastmousePosition;
    // Start is called before the first frame update
    void Start()
    {
    
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.back * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * Mathf.Rad2Deg * Time.deltaTime * lookspeed);
        Camera.main.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * Mathf.Rad2Deg * Time.deltaTime * lookspeed);
    }
}
