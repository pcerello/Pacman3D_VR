using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
<<<<<<< 445f51281dfcaf9a2868c38d8bbcf74c353610e0
using UnityEngine.XR.Interaction.Toolkit;
=======
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SceneManagement;
>>>>>>> 82b39237abbe714447c3aec94fb139f502a1dd4e

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float runSpeed;

<<<<<<< 445f51281dfcaf9a2868c38d8bbcf74c353610e0
    [SerializeField] private ContinuousMoveProviderBase movement;

    private Vector2 _movement;
    private float _initial_speed;
    private bool _is_running = false;
=======

>>>>>>> 82b39237abbe714447c3aec94fb139f502a1dd4e
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

    public void OnRun()
    {
        _is_running = !_is_running;
        if (_is_running)
        {
            movement.moveSpeed = runSpeed;
        }
        else
        {
            movement.moveSpeed = _initial_speed;
        }
    }
}
