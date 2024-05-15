using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private bool invertSpriteOnLeft;
    [SerializeField] private bool outputDebug;


    Vector2 lastDirection;
    private void Awake()
    {
        playerMovement.onPlayerMoved += OnPlayerMoved;
        playerMovement.onPlayerStoppedMoving += OnPlayerStoppedMoving;
    }

    void Start()
    {
        
    }

    void OnPlayerMoved(Vector2 direction)
    {
        lastDirection = direction;

        if(outputDebug)
        Debug.Log("Setting" + true + " " + direction);

        animator.SetBool("Walking", true);
        animator.SetFloat("XSpeed", direction.x);
        animator.SetFloat("YSpeed", direction.y);

        CheckSpriteInversion(direction);
    }

    void CheckSpriteInversion(Vector2 movementDirection)
    {
        if (!invertSpriteOnLeft)
            return;

        var scale = transform.localScale;
        if (movementDirection.x < 0)
        {
            scale.x = -1;
        }
        else if (movementDirection.x > 0)
        {
            scale.x = 1;
        }
        transform.localScale = scale;
    }
    void OnPlayerStoppedMoving()
    {
        if (outputDebug)
            Debug.Log("Setting" + false);
        animator.SetBool("Walking", false);
    }
}
