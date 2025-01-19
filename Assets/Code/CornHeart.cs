using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornHeart : MonoBehaviour
{
    public Cornstalker cornstalker;
    public GameObject corn;
    public GameObject popcornHeart;
    public MainMenu winner;
    private float currentTime = 0f;
    private int currentSecond = 0;
    private float loopTime = 0f;
    private float rollAgain = 0.25f;
    public bool popped;
    private bool didThat;
    private bool didThatAsWell;

    // Start is called before the first frame update
    void Start()
    {
        popped = false;
        didThat = false;
        didThatAsWell = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        loopTime += Time.deltaTime;
        if(popped && !didThat){
            currentTime = 0;
            loopTime = 0;
            cornstalker.Die();
            AudioManager.main.Play("Charge Up");
            didThat = true;
        }
        if (popped){
            Pop();
        }
    }

    public void Pop(){
        if (loopTime > rollAgain){
            loopTime = 0f;
            this.transform.rotation = Random.rotation;
        }
        if (Mathf.FloorToInt(currentTime) > currentSecond){
            currentSecond++;
            rollAgain -= 0.025f;
            if (rollAgain <= 0f){
               rollAgain = 0.025f;
            }
        }
        if (currentTime >= 20f && !didThatAsWell){
            GameObject pop = Instantiate(popcornHeart, GetComponent<Transform>().position, Quaternion.identity);
		    AudioManager.main.Play("Pop");
            if (corn.TryGetComponent<MeshRenderer>(out MeshRenderer rend))
		    {
			    rend.enabled = false;
		    }
            didThatAsWell = true;
        }
        if (currentTime >= 25f){
            winner.win = true;
        }
    }
}
