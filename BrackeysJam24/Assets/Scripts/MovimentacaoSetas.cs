using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentacaoSetas : MonoBehaviour
{
    [SerializeField] private float _velocity = 3;
    [SerializeField] private float _jumpVelocity = 10;
    private Rigidbody2D _rigidbody2D;
    private Inputs _inputs;
    private Vector2 _velocity2D;
    private bool _jumpPerformed = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputs = GameManager.Instance.InputActions;
        _inputs.Player.Movement.performed += MovePlayer;
        _inputs.Player.Movement.canceled += MovePlayer;
        _inputs.Player.Jump.performed += JumpPlayer;
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        float xDirection = context.ReadValue<float>();
        _velocity2D = _rigidbody2D.velocity;
        _velocity2D.x = xDirection * _velocity;
        _rigidbody2D.velocity = _velocity2D;
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        if (_jumpPerformed)
        {
            //_jumpPerformed = false;
            _velocity2D = _rigidbody2D.velocity;
            _velocity2D.y = _jumpVelocity;
            _rigidbody2D.velocity = _velocity2D;
        }
    }
}
