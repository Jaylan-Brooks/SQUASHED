using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signjacker : MonoBehaviour
{
    public GameObject sign;
    public GameObject deathPop;
    public float rightWay;

    public void Die()
	{
        sign.transform.rotation = Quaternion.Euler(0, rightWay, 0);
		GameObject burst = Instantiate(deathPop, GetComponent<Transform>().position, Quaternion.identity);
		AudioManager.main.Play("Pumpkin Guts");
        AudioManager.main.Play("Sign Creak");
		Destroy(gameObject);
        return;
	}
}
