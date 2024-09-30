using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour, IEventReceiver<InteractEvent>,IEventReceiver<TeleportEvent>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpForce;

    [SerializeField] private EventBus _eventBus;

    private bool _canMove = true;

    private Vector3 _jumpVector;

    private CharacterController _character;

    private PlayerInputActions _inputActions;

    public static bool IsGrounded = true;
    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<InteractEvent>);
        _eventBus.Register(this as IEventReceiver<TeleportEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<InteractEvent>);
        _eventBus.UnRegister(this as IEventReceiver<TeleportEvent>);
    }   

    private void Start()
    {
        _character = GetComponent<CharacterController>();

        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Jump.performed += Jump;

    }
    public void Jump(InputAction.CallbackContext context)  
    {
        if(IsGrounded)
            _jumpVector.y = (float)Math.Sqrt(_jumpForce * _gravity);
    }

    private void Update()
    {
        if (_canMove)
        {
            _character.Move((transform.right * -_inputActions.Player.Movement.ReadValue<Vector2>().x +
               transform.forward * -_inputActions.Player.Movement.ReadValue<Vector2>().y).normalized * _speed * Time.deltaTime);

            _jumpVector.y -= _gravity * Time.deltaTime;

            _character.Move(_jumpVector * Time.deltaTime);

            IsGrounded = _character.isGrounded;
        }
    }

    public void OnEvent(InteractEvent @event)
    {
        StartCoroutine(Waiting(@event.Delay));
    }
    private IEnumerator Waiting(float delay)
    {
        /*float speed = _speed;
        float jumpForce = _jumpForce;

        _speed = 0;
        _jumpForce = 0;*/
        _canMove = false;

        yield return new WaitForSeconds(delay);

        _canMove = true;

     /*   _speed = speed;
        _jumpForce = jumpForce;*/

    }

    public void OnEvent(TeleportEvent @event)
    {
        StartCoroutine(Waiting(1));
    }
}
