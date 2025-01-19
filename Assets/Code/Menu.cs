using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool paused = false;
    private bool muted = false;
    public bool win = false;

    public InputManager inputManager;

    public GameObject UI;
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;

    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (win){
            Win();
        }

        if ((Input.GetKeyDown(KeyCode.P)) && !Health.farmer.dead && !win){
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.M)){
            Mute();
        }

        if (Input.GetKeyDown(KeyCode.R) && !win){
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.R) && win){
            Play();
            if (win){
                win = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab)){
            Menu();
            if (win){
                win = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete)){
            Quit();
            if (win){
                win = false;
            }
        }

    }

    public void Play(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SQUASHED");
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win(){
        winMenuUI.SetActive(true);
        UI.SetActive(false);
        inputManager.enabled = false;
        Time.timeScale = 0f;
    }

    public void Pause(){
        if (paused){
            pauseMenuUI.SetActive(false);
            inputManager.enabled = true;
            Time.timeScale = 1f;
            paused = false;
        }
        else {
            pauseMenuUI.SetActive(true);
            inputManager.enabled = false;
            Time.timeScale = 0f;
            paused = true;
        }
    }

    public void Mute(){
        if (muted){
            AudioListener.volume = 1;
            muted = false;
        }
        else {
            AudioListener.volume = 0;
            muted = true;
        }
    }

    public void Menu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit(){
        Application.Quit();
    }
}
