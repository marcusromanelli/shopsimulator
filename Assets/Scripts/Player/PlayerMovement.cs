using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    public delegate void OnPlayerMoved(Vector2 direction);
    public event OnPlayerMoved onPlayerMoved;
    public delegate void OnPlayerStoppedMoving();
    public event OnPlayerStoppedMoving onPlayerStoppedMoving;
    [SerializeField] private Grid gridComponent;

    [SerializeField] private float speed;



    private Vector2 lastTargetDirection;
    private Rigidbody2D rigidbody;
    private Vector3 lastDirection;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OnMove(lastTargetDirection);
    }

    public void OnReceivedMovement(InputAction.CallbackContext context)
    {
        var targetMovement = context.ReadValue<Vector2>();

        lastTargetDirection = targetMovement;

        if (targetMovement == Vector2.zero)
            OnStoppedMovement();
    }
    public void OnStoppedMovement()
    {
        onPlayerStoppedMoving?.Invoke();
    }
    public Vector3Int GetPosition()
    {
        return gridComponent.WorldToCell(transform.position);
    }
    public Vector3Int GetLastDirection()
    {
        return Vector3Int.FloorToInt(lastDirection);
    }
    void OnMove(Vector3 target)
    {
        if (target == Vector3.zero)
            return;

        Vector3 position = transform.position;

        position += target * speed;

        CalculateDirection(position, transform.position);

        rigidbody.MovePosition(position);

        onPlayerMoved?.Invoke(target);
    }

    void CalculateDirection(Vector3 newPosition, Vector3 lastPosition)
    {
        lastDirection = (newPosition - lastPosition).normalized;

    }
}
