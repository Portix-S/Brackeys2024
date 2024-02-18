using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float velocity = 3;
    [SerializeField] private float jumpVelocity = 10;
    [SerializeField] private float isGroundedHeight = 0.5f;
    [SerializeField] private float isGroundedWidthOffset = 0.1f;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private bool inverter;
    private Rigidbody2D _rigidbody2D;
    private Inputs _inputs;
    private BoxCollider2D _boxCollider2D;
    private Transform _transform;
    private float _direction;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _transform = GetComponent<Transform>();
        
        _inputs = GameManager.Instance.InputActions;
        _inputs.Player.Movement.performed += SetDirections;
        _inputs.Player.Movement.canceled += SetDirections;
        _inputs.Player.Jump.performed += JumpPlayer;
    }

    private void Update()
    {
        Vector2 velocity2D = _rigidbody2D.velocity;
        velocity2D.x = _direction * velocity;
        _rigidbody2D.velocity = velocity2D;
    }

    private void SetDirections(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<float>();
        if (_direction != 0)
        {
            Vector3 scale = _transform.localScale;
            scale.x = inverter != _direction > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            _transform.localScale = scale;
        }
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            Vector2 velocity2D = _rigidbody2D.velocity;
            velocity2D.y = jumpVelocity;
            _rigidbody2D.velocity = velocity2D;
        }
    }

    private bool IsGrounded()
    {
        Bounds colliderBounds = _boxCollider2D.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(colliderBounds.center,
            colliderBounds.size - new Vector3(isGroundedWidthOffset, 0, 0), 0f, Vector2.down, isGroundedHeight, platformLayerMask);

        return raycastHit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Bounds colliderBounds = _boxCollider2D.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(colliderBounds.center,
            colliderBounds.size - new Vector3(isGroundedWidthOffset, 0, 0), 0f, Vector2.down, isGroundedHeight, platformLayerMask);
        
        Gizmos.color = raycastHit.collider != null ? Color.green : Color.red;
        
        Gizmos.DrawWireCube(colliderBounds.center + Vector3.down * isGroundedHeight,
            colliderBounds.size - new Vector3(isGroundedWidthOffset, 0, 0));
    }
}
