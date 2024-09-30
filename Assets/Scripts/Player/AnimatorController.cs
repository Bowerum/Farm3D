using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatorController : MonoBehaviour, IEventReceiver<InteractEvent>
{
    private Animator _animator;

    [SerializeField] private EventBus _eventBus;

    private PlayerInputActions _inputActions;

    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<InteractEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<InteractEvent>);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (MovementController.IsGrounded)
            _animator.SetTrigger("jump");
    }

    private void Update()
    {
        _animator.SetFloat("x", _inputActions.Player.Movement.ReadValue<Vector2>().x);
        _animator.SetFloat("z", _inputActions.Player.Movement.ReadValue<Vector2>().y);

        _animator.SetBool("fall",!MovementController.IsGrounded);
    }
    public void OnEvent(InteractEvent @event)
    {
        _animator.SetTrigger("interact");
        _animator.SetFloat("animation", @event.Animation);
    }
}
