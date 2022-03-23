using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] Game game;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            game.community = 0;
            game.wealth = 40;
            game.playThrough = "Rich Fool";
            SceneManager.LoadScene("Test_Char");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            game.wealth = 20;
            game.community = 40;
            game.playThrough = "Community Leader";
            SceneManager.LoadScene("Test_Char");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
