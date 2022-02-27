using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wealth : MonoBehaviour
{

    public int currentWealth = 1;
    public float coeff = 1.0f;

    private float initialSpeed;

    private bool picking;
    private Animator animator;

    public void Start()
    {
        initialSpeed = GetComponent<ThirdPersonMovement>().speed;
        animator = GetComponent<Animator>();
    }

    public void updateSpeed() {
        GetComponent<ThirdPersonMovement>().speed = initialSpeed + coeff * currentWealth;
        currentWealth += 1;

        animator.SetBool("Picking", true);
        Invoke("RevertPicking", 1);
    }

    private void RevertPicking() { 
        animator.SetBool("Picking", false);
    }
}
