using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Gourds playerInput;
    public Gourds.PlayerActions actions;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new Gourds();
        actions = playerInput.Player;
    }

    // Update is called once per frame
    public Gourds.PlayerActions ReturnActions()
    {
        return actions;
    }

    private void OnEnable(){
        actions.Enable();
    }

    private void OnDisable(){
        actions.Disable();
    }
}
