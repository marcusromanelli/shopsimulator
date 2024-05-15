using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public delegate void OnPlayerMoved(Vector2 direction);
    public event OnPlayerMoved onPlayerMoved;
    public delegate void OnPlayerStoppedMoving();
    public event OnPlayerStoppedMoving onPlayerStoppedMoving;

    [SerializeField] private float speed;



    private Vector2 lastTargetDirection;
    private Rigidbody2D rigidbody;

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
    void OnMove(Vector3 target)
    {
        if (target == Vector3.zero)
            return;

        Vector3 position = transform.position;

        position += target * speed;

        rigidbody.MovePosition(position);

        onPlayerMoved?.Invoke(target);
    }
}
