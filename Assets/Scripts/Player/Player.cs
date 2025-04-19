using UnityEngine;
using StarterAssets_InputSystem;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public StarterAssetsInputs _input { get; private set; }
    private CharacterController _controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _input = GetComponent<StarterAssetsInputs>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.currentAction.Equals("Horse", StringComparison.OrdinalIgnoreCase))
        {
            _controller.enabled = false;
        }
        else
        {
            _controller.enabled = true;
        }
    }
}
