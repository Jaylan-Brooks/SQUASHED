using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlatMenu : MonoBehaviour
{
    private bool muted = false;

    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
            Play();
        }

        if (Input.GetKeyDown(KeyCode.M)){
            Mute();
        }

        if (Input.GetKeyDown(KeyCode.Tab)){
            Menu();
        }

        if (Input.GetKeyDown(KeyCode.Delete)){
            Quit();
        }
    }

    public void Play(){
        SceneManager.LoadScene("SQUASHED");
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

    public void Controls(){
        SceneManager.LoadScene("Controls");
    }

    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void Quit(){
        Application.Quit();
    }
}
