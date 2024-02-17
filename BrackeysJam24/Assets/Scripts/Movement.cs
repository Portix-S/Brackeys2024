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
    private Rigidbody2D _rigidbody2D;
    private Inputs _inputs;
    private BoxCollider2D _boxCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        
        _inputs = GameManager.Instance.InputActions;
        _inputs.Player.Movement.performed += MovePlayer;
        _inputs.Player.Movement.canceled += MovePlayer;
        _inputs.Player.Jump.performed += JumpPlayer;
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 velocity2D = _rigidbody2D.velocity;
        velocity2D.x = context.ReadValue<float>() * velocity;
        _rigidbody2D.velocity = velocity2D;
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
