using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public float range = 30f;
    public LayerMask mask;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.actions.Fire.triggered && AmmoCount.farmer.GetAmmo() > 0){
            Shoot();
        }
    }

    public void Shoot(){
        AmmoCount.farmer.Use();
        AudioManager.main.Play("Shoot");
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, range, mask)){
            if (hitInfo.collider.gameObject.tag == "Pumpkinhead"){
                Pumpkinhead pumpkinhead = hitInfo.collider.GetComponent<Pumpkinhead>();
                pumpkinhead.Damage();
            }
            if (hitInfo.collider.gameObject.tag == "Head"){
                Head head = hitInfo.collider.GetComponent<Head>();
                head.Headshot();
            }
            if (hitInfo.collider.gameObject.tag == "Signjacker"){
                Signjacker signjacker = hitInfo.collider.GetComponent<Signjacker>();
                signjacker.Die();
            }
            if (hitInfo.collider.gameObject.tag == "Cornstalker"){
                Cornstalker cornstalker = hitInfo.collider.GetComponent<Cornstalker>();
                cornstalker.Damage();
            }
            if(hitInfo.collider.gameObject.tag == "CornHeart"){
                CornHeart cornHeart = hitInfo.collider.GetComponent<CornHeart>();
                cornHeart.popped = true;
            }
        }
    }
}
