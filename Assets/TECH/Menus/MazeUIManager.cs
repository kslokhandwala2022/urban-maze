using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI endContent;
    [SerializeField] GameObject end;
    [SerializeField] Game game;

    //UI
    [SerializeField] TextMeshProUGUI wealthAmt;
    [SerializeField] TextMeshProUGUI timer;


    [Header("Tier Image")]
    public Sprite untriggeredCoinSprite;
    public Sprite triggeredCoinSprite;
    public GameObject tierImageParent;
    public GameObject coinImage;
    public Image[] coinImages;
    public GameObject mainMenu;
    public GameObject newRunButton;
    public GameObject resumeRunButton;

    private float timeVal;
    private string timerString;
    private bool timeStarted;

    public void InitGUI()
    {
        timeVal = 0;
        timer.gameObject.SetActive(true);
        end.SetActive(false);

        //init speed tiers
        for(int i = 0; i < game.speedTiers; i++)
        {
            Instantiate(coinImage, tierImageParent.transform);
        }
        coinImages = tierImageParent.GetComponentsInChildren<Image>();
    }

    // Called from the main menu start button to start the timer
    public void StartTrackingTime()
    {
        timeStarted = true;
    }

    public void UpdateWealth()
    {
        wealthAmt.text = "" + game.playerWealth;
        UpdateSpeedTiers();

    }
    public void UpdateTimer()
    {
        if(timeStarted)
        {
            timeVal += Time.deltaTime;
            timer.text = TimeToString(timeVal);
            
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ReturntoMenu(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturntoMenu(false);
        }
    }

    private void UpdateSpeedTiers()
    {
        // Update Image
        for (int i = 0; i < coinImages.Length; i++)
        {
            coinImages[i].sprite = i <= game.getSpeedTier() ? triggeredCoinSprite : untriggeredCoinSprite;
        }
    }

    public void EndMaze()
    {
        end.SetActive(true);
        timeStarted = false;
        endContent.text = "Total Time: " + TimeToString(timeVal) + 
            "\nWealth Kept: " + game.playerWealth +
            "\nWealth Donated: " + (game.numObjectsFixed * 5);
    }

    private string TimeToString(float time)
    {
        float minutes = Mathf.Floor(timeVal / 60);
        float seconds = Mathf.RoundToInt(timeVal % 60);
        string min = minutes.ToString();
        string sec = seconds.ToString();

        if (minutes < 10)
        {
            min = "0" + minutes.ToString();
        }
        if (seconds < 10)
        {
            sec = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        return min + ":" + sec;
    }


    public void ReturntoMenu(bool isEnd)
    {
        if (isEnd)
        {
            // reset scene if you've finished the maze
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // pause at the menu if you have not finished the maze
            mainMenu.SetActive(true);
            newRunButton.SetActive(false);
            resumeRunButton.SetActive(true);
        }
    }
}
