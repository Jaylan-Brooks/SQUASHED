using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image sr;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;

    [Header ("Attributes")]
    [SerializeField] private Sprite[] pages;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.RightArrow) && SceneManager.GetActiveScene().buildIndex == 2){
           Next(); 
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && SceneManager.GetActiveScene().buildIndex == 2){
           Previous(); 
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
           Menu(); 
        }
    }

    public void Next(){
        index++;
        if (index > pages.Length-1){
            index = pages.Length-1;
        }
        sr.sprite = pages[index];
        nextButton.SetActive(false);
        previousButton.SetActive(true);
    }

    public void Previous(){
        index--;
        if (index < 0){
            index = 0;
        }
        sr.sprite = pages[index];
        nextButton.SetActive(true);
        previousButton.SetActive(false);
    }

    public void Menu(){
        SceneManager.LoadScene("Main Menu");
    }
}
