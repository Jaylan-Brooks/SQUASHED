using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public static Health farmer;
    [SerializeField]
    private int health;
    [SerializeField]
    private TextMeshProUGUI healthCounter;

    [Header("Game Over")]
    public GameObject deathScreen;
    public GameObject mainScreen;
    public InputManager inputManager;

    public bool dead;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;

    private void Awake(){
        farmer = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        Time.timeScale = 1f;
        inputManager.enabled = true;
        dead = false;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 10){
            health = 10;
        }
        healthCounter.text = "HEALTH: " + health;
        if (health <= 0){
            Die();
        }
        if (overlay.color.a > 0){
            durationTimer += Time.deltaTime;
            if (durationTimer > duration){
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void Damage(){
        health--;
        AudioManager.main.Play("Hit");
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void BigDamage(){
        health -= 5;
        AudioManager.main.Play("Hit");
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void Heal(){
        health += 2;
    }

    public void Die(){
        health = 0;
        AudioManager.main.Play("Death");
        dead = true;
        deathScreen.SetActive(true);
        mainScreen.SetActive(false);
        inputManager.enabled = false;
        Time.timeScale = 0f;
    }
}
