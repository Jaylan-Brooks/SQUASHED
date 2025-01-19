using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact(){
        AmmoCount.farmer.Reload();
        AudioManager.main.Play("Reload");
        Destroy(gameObject);
        return;
    }
}
