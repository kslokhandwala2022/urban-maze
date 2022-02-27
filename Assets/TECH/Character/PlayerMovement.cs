#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    RaycastHit rayHit;

    Vector3 velocity;
    public float movingThreashold = 0.25f;
    public float runningThreashold = 10f;
    bool isRunning;
    bool isMoving;
    bool isGrounded;
    bool isJumping;
    bool isFalling;

#if ENABLE_INPUT_SYSTEM
    InputAction movement;
    InputAction jump;

    Animator animator;

    void Start()
    {
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");
        
        jump = new InputAction("PlayerJump", binding: "<Gamepad>/a");
        jump.AddBinding("<Keyboard>/space");

        movement.Enable();
        jump.Enable();

        animator = GetComponent<Animator>();
    }
#endif

    // Update is called once per frame
    void Update()
    {
        float x;
        float z;
        bool jumpPressed = false;

#if ENABLE_INPUT_SYSTEM
        var delta = movement.ReadValue<Vector2>();
        x = delta.x;
        z = delta.y;
        jumpPressed = Mathf.Approximately(jump.ReadValue<float>(), 1);
#else
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");
#endif

        if (Physics.Raycast(transform.position, -Vector3.up, out rayHit)) {
            if (rayHit.transform.gameObject.tag == "Ground") {
                groundCheck = rayHit.transform.gameObject.transform;
                isGrounded = rayHit.distance < groundDistance;
            }
        
        }

        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            if (isFalling) {
                isFalling = false;
                animator.SetBool("Falling", false);
                isJumping = false;
            }

            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;


        isMoving = move.magnitude > movingThreashold;
        isRunning = speed > runningThreashold;

        animator.SetBool("Moving", isMoving);
        animator.SetBool("Grounded", isGrounded);

        if (isMoving && isRunning)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        controller.Move(move * speed * Time.deltaTime);

        if(jumpPressed && isGrounded)
        {
            isJumping = true;
            animator.SetBool("Jumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if (isJumping && velocity.y < 0.1)
        {
            isFalling = true;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
