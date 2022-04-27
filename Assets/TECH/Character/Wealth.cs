using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wealth : MonoBehaviour
{

    public Game game;
    private Animator animator;


    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void updateSpeed() {
        game.playerWealth++;
    }

    private void RevertPicking() { 
        animator.SetBool("Picking", false);
    }
}
