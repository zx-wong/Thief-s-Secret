using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public CharacterController playerController;

    public float rotateSpeed = 2.0f;
    public float speed = 5f;
    public float jumpHeight = 3f;

    //velocty
    Vector3 velocity;
    public float gravity = -9.81f;

    //ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    string horizontal = "Horizontal";
    string vertical = "Vertical";
    string jump = "Jump";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis(horizontal);
        float z = Input.GetAxis(vertical);

        if (Input.GetAxis(vertical) > 0)
        {
            transform.position += Vector3.forward * Time.deltaTime;
            playerAnimator.SetBool("walking", true);
        }
        else 
        {
            playerAnimator.SetBool("walking", false);
        }

        if (Input.GetAxis(vertical) < 0)
        {
            transform.position += Vector3.back * Time.deltaTime;
            playerAnimator.SetBool("walkBackward", true);
        }
        else
        {
            playerAnimator.SetBool("walkBackward", false);
        }

        if (Input.GetButtonDown(jump) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis(horizontal) * rotateSpeed, 0);

        Vector3 move = transform.forward * z;
        playerController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }
}   