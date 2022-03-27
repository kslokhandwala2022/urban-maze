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
            game.playerCommunity = 0;
            game.playerWealth = 40;
            game.playThrough = "Rich Fool";
            SceneManager.LoadScene("Test_Char");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            game.playerWealth = 20;
            game.playerCommunity = 40;
            game.playThrough = "Community Leader";
            SceneManager.LoadScene("Test_Char");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
