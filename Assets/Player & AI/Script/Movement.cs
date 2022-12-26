using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator playerAnimator;

    public CharacterController controller;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float rotateSpeed = 0.5f;

    public bool isCrouching = false;
    public bool isSneaking = false;
    public bool isWalking = false;
    public bool isRunning = false;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float currentSpeed = speed * Input.GetAxis("Vertical");
        float sneakSpeed = (speed - 2.5f) * Input.GetAxis("Vertical");

        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            controller.Move(forward * currentSpeed * Time.deltaTime);
            isWalking = true;
            playerAnimator.SetBool("isWalk", true);

            if (isRunning)
            {
                playerAnimator.SetBool("isRun", true);
            }
            else
            {
                playerAnimator.SetBool("isRun", false);
            }
        }
        else
        {
            isWalking = false;
            playerAnimator.SetBool("isWalk", false);
        }

        if (Input.GetButtonDown("Ctrl"))
        {
            if (!isSneaking)
            {
                if (isCrouching)
                {
                    controller.center = new Vector3(0, 2.8f, 0);
                    controller.height = 5.55f;
                    isCrouching = false;
                    isWalking = true;
                }
                else
                {
                    controller.center = new Vector3(0, 1.5f, 0);
                    controller.height = 3.3f;
                    isCrouching = true;
                    isWalking = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 9.0f;
            isWalking = true;
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5.0f;
            isWalking = true;
            isRunning = false;
        }

        if (isCrouching)
        {
            playerAnimator.SetBool("isCrouch", true);

            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                isWalking = false;
                isCrouching = true;
                isSneaking = true;
                controller.Move(forward * sneakSpeed * Time.deltaTime);
                playerAnimator.SetBool("isSneak", true);
            }
            else
            {
                isWalking = false;
                isCrouching = true;
                isSneaking = false;
                playerAnimator.SetBool("isSneak", false);
            }
        }
        else
        {
            isWalking = true;
            isCrouching = false;
            isSneaking = false;
            playerAnimator.SetBool("isCrouch", false);
        }

        if (isDead)
        {
            controller.detectCollisions = false;
            controller.enabled = false;
            playerAnimator.SetBool("isDead", true);
            Debug.Log("Player Being Electrocuted");
        }
    }
}
