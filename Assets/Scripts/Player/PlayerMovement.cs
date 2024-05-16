using System;
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
    private Action onFinishForceMovement;
    private bool isForcingMovement;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OnMove(lastTargetDirection);

        CheckPositionChanged();
    }

    void CheckPositionChanged()
    {
        if (rigidbody.velocity.magnitude > 0)
            return;

        onFinishForceMovement?.Invoke();
        onFinishForceMovement = null;
        isForcingMovement = false;
    }

    public void OnReceivedMovement(InputAction.CallbackContext context)
    {
        if (isForcingMovement)
            return;

        var targetMovement = context.ReadValue<Vector2>();

        lastTargetDirection = targetMovement;

        if (targetMovement == Vector2.zero)
            OnStoppedMovement();
    }
    void OnStoppedMovement()
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
    public void ForceMovement(Vector3 targetOffsetPosition, Action onFinish)
    {
        isForcingMovement = true;
        lastTargetDirection = targetOffsetPosition;
        this.onFinishForceMovement = onFinish;
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
