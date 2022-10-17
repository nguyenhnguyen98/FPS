using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;

    private PlayerManager player;
    private PlayerLook look;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        player = GetComponent<PlayerManager>();
        look = GetComponent<PlayerLook>();

        _onFoot.Jump.performed += ctx => player.Jump();
        _onFoot.Sprint.performed += ctx => player.Sprinting();
        _onFoot.Crouch.performed += ctx => player.Crouch();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.ProcessMove(_onFoot.Movement.ReadValue<Vector2>(), Time.deltaTime);
    }

    void LateUpdate()
    {
        look.ProcessLook(_onFoot.Look.ReadValue<Vector2>(), Time.deltaTime);
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
