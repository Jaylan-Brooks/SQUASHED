using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Pumpkinhead pumpkinhead;
    public GameObject deathPop;

    public void Headshot()
	{
		GameObject burst = Instantiate(deathPop, GetComponent<Transform>().position, Quaternion.identity);
		pumpkinhead.Headshot();
	}
}
