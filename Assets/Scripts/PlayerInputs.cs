using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    Controls controls;
    public static PlayerInputs instance { get; private set; }
    public bool sprint = false;
    public Vector2 moveInput { get; private set; }
    public event Action Jumping; 

    protected void Awake()
    {
        controls = new Controls();
        controls.Enable();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        controls.Ground.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Ground.Movement.canceled += ctx => Move(Vector2.zero);
        controls.Ground.Jump.started += ctx => Jump();
        controls.Ground.Sprint.started += ctx => Sprint();
        controls.Ground.Sprint.canceled += ctx => Sprint();
    }

    private void Sprint()
    {
        sprint = !sprint;
        Debug.Log("Sprint Status:"+sprint);
    }

    private void Jump()
    {
        Jumping?.Invoke();
        Debug.Log("Jumped");
    }

    private void Move(Vector2 direction)
    {
        // Handle movement input
        moveInput = direction;
    }
}
