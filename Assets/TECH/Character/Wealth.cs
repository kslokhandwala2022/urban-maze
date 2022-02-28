using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wealth : MonoBehaviour
{

    public int currentWealth = 0;
    public float coeff = .3f;

    private float initialSpeed;

    private bool picking;
    private Animator animator;
    private PlayerMovement playerMovement;

    public void Start()
    {
        // initialSpeed = GetComponent<ThirdPersonMovement>().speed;
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void updateSpeed() {
        // GetComponent<ThirdPersonMovement>().speed = initialSpeed + coeff * currentWealth;
        playerMovement.speed += coeff;
        currentWealth += 1;

        animator.SetBool("Picking", true);
        Invoke("RevertPicking", 1);
    }

    private void RevertPicking() { 
        animator.SetBool("Picking", false);
    }
}
