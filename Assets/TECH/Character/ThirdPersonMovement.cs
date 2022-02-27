using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 1f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool moving;
    private bool running;

    public Vector2 _look;
    public Quaternion nextRotation;
    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;
    public GameObject followTransform;

    Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        moving = false;
        running = false;

        if (direction.magnitude >= 0.1f) {

            moving = true;
            if (speed > 5f) running = true;

            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angleMove = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angleMove, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        updateAnimator();
    }

    private void updateAnimator() {
        animator.SetBool("Moving", moving);
        animator.SetBool("Running", running);
    }
}
