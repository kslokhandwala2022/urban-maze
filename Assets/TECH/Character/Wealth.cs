using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wealth : MonoBehaviour
{

    public Game game;

    private bool picking;
    private Animator animator;
    private ThirdPersonMovement playerMovement;

    #region Speed Tier Related
    [Header("Speed Tier Related")]
    
    // General Configurations
    public int numberOfTiers = 4;
    public int currentTier = 1;

    // Tier threasholds of wealth for changing tiers
    [Header("Speed Tier Threashold")]
    public int fixedTierThreashold = 7;                 // Fixed update number for tier threadsholds (after X coins update tier)
    public bool usingFixedThreasholds = true;           // Bool to switch custom and fixed tier threashold
    private int threadsholdCounter = 0;                 // The counter to automatically update threashold if using fixed threashold
    public int[] customTierThreasholds;                 // Array for customized tier changing threasholds
    private int currentThreasholdIndex = 0;

    [Header("Speed Change")]
    // For speed change
    public float speedMultiplier = 1.5f;                // For automatic fixed speed change between tiers
    public bool usingFixedMutilplier = true;            // Bool to switch custom and fixed speed multiplier
    public float[] customSpeedChanges;                  // For customized speed change between tiers
    private int currentSpeedChangeIndex = 0;

    // For image update
    [Header("Tier Image")]
    public Sprite triggeredCoinSprite;
    public GameObject tierImageParent;
    public Image[] coinImages;

    #endregion

    public void Start()
    {
        // initialSpeed = GetComponent<ThirdPersonMovement>().speed;
        playerMovement = GetComponent<ThirdPersonMovement>();
        animator = GetComponent<Animator>();

        // Get coin images
        coinImages = tierImageParent.GetComponentsInChildren<Image>();
    }

    public void updateSpeed() {
        game.playerWealth++;

        // Update Tier Related
        threadsholdCounter++;
        if (usingFixedThreasholds) {
            if (threadsholdCounter > fixedTierThreashold) {
                updateTier();
            }
        } else {
            try {
                if (threadsholdCounter > customTierThreasholds[currentThreasholdIndex]) {
                    currentThreasholdIndex++;
                    updateTier();
                }
            }
            catch {
                Debug.LogError("Player::Wealth: Custom Wealth Threashold Error");
            }
        }
    }

    private void RevertPicking() { 
        animator.SetBool("Picking", false);
    }

    private void updateTier() {

        if (currentTier >= numberOfTiers) return;

        // Update Speed
        if (usingFixedMutilplier) {
            playerMovement.speed *= speedMultiplier;
        } else {
            playerMovement.speed *= customSpeedChanges[currentSpeedChangeIndex];
            currentSpeedChangeIndex ++;
        }

        // Update Image
        coinImages[currentTier].sprite = triggeredCoinSprite;       // Do not need to subtract 1 because the first index to update is 1
        currentTier++;

    }
}
