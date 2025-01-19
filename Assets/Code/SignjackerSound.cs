using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignjackerSound : MonoBehaviour
{
    private bool detected;
    public string soundEffect;

    void Start(){
        detected = false;
    }

    public bool IsThere(){
        return detected;
    }

    private void OnTriggerEnter (Collider collider){
        if (collider.gameObject.tag == "Player"){
            if(soundEffect == "Laugh"){
                float random = Random.value;
                if (random <= 0.20f){
                    AudioManager.main.Play("Laugh 1");
                }
                if (random > 0.20f && random <= 0.40f){
                    AudioManager.main.Play("Laugh 2");
                }
                if (random > 0.40f && random <= 0.60f){
                    AudioManager.main.Play("Laugh 3");
                }
                if (random > 0.60f && random <= 0.70f){
                    AudioManager.main.Play("Laugh 4");
                }
                if (random > 0.70f && random <= 0.80f){
                    AudioManager.main.Play("Laugh 5");
                }
                if (random > 0.80f && random <= 0.90f){
                    AudioManager.main.Play("Laugh 6");
                }
                if (random > 0.90f){
                    AudioManager.main.Play("Laugh 7");
                }
            }
            else {
                AudioManager.main.Play(soundEffect);
            }
        }
    }
}
