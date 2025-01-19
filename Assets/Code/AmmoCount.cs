using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public static AmmoCount farmer;
    [SerializeField]
    private int ammo = 12;
    [SerializeField]
    private TextMeshProUGUI ammoCounter;

    private void Awake(){
        farmer = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (ammo > 999){
            ammo = 999;
        }
        if (ammo < 0){
            ammo = 0;
        }
        ammoCounter.text = "AMMO: " + ammo;
    }

    public void Use(){
        ammo--;
    }

    public void Reload(){
        ammo += 3;
    }

    public int GetAmmo(){
        return ammo;
    }
}
