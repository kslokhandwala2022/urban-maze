using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Fixable : MonoBehaviour
{

    [SerializeField] GameObject modelFixed;
    [SerializeField] GameObject modelBroken;
    [SerializeField] string type;
    [SerializeField] int cost;
    [SerializeField] TextMeshPro interactbleText;
    [SerializeField] Game game;


    // Start is called before the first frame update
    void Start()
    {
        SetInteractableText();
        SetBroken();
        interactbleText.gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactbleText.gameObject.transform.DOScale(1, .3f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(game.wealth >= cost)
                {
                    game.wealth -= cost;
                    SetFixed();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactbleText.gameObject.transform.DOScale(0, .3f);
        }
    }

    void SetInteractableText()
    {
        interactbleText.text = $"Fix {type} ({cost} wealth)";
    }

    void SetBroken()
    {
        modelBroken.SetActive(true);
        modelFixed.SetActive(false);
    }

    void SetFixed()
    {
        modelBroken.SetActive(false);
        modelFixed.SetActive(true);
        interactbleText.gameObject.SetActive(false);
    }
}
