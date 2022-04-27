using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    //Base Movement
    [Header("Movement")]
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Transform cam;


    //Game
    [Header("Game")]
    public Game game;
    public SkinnedMeshRenderer playerMesh;

    //animation
    [Header("Health Settings")]
    Animator animator;
    public float movingThreshold = 0.25f;
    public float runningThreshold = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            //rotate player on y axis
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            //move in direction of player orientation
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerMesh.material.color = game.getPlayerColor();
            playerMesh.material.SetColor("_EmissionColor", game.getPlayerEmmisiveColor());

            //game.getPlayerColor();
            // Vector3 move = moveDir.normalized * (speed + (wealthToSpeed * game.playerWealth));
            float currentSpeed = speed * game.getSpeedMultiplier(); 
            Vector3 move = moveDir.normalized * (currentSpeed);
            controller.Move(move * Time.deltaTime);

            animator.SetBool("Moving", move.magnitude > movingThreshold);
            animator.SetBool("Running", move.magnitude > runningThreshold);
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }


    }
}
