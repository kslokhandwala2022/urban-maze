using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wealth : MonoBehaviour
{

    public Game game;

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
        game.playerWealth++;
    }

    private void RevertPicking() { 
        animator.SetBool("Picking", false);
    }
}
