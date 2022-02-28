using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            //go back to start
            FindObjectOfType<MazeUIManager>().EndMaze();
            Debug.Log("Game Finish");
        }
    }
}
