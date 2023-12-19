using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float runSpeed;

    [SerializeField] private ContinuousMoveProviderBase movement;

    private Vector2 _movement;
    private float _initial_speed;
    private bool _is_running = false;
    private void Start()
    {
        _initial_speed = movement.moveSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(_movement.x, 0, _movement.y) * speed);
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            movement.moveSpeed = runSpeed;
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            movement.moveSpeed = _initial_speed;
        }
        /**
        _is_running = !_is_running;
        if (_is_running)
        {
            movement.moveSpeed = runSpeed;
        }
        else
        {
            movement.moveSpeed = _initial_speed;
        }*/
    }
}
