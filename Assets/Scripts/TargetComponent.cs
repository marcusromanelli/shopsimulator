using UnityEngine;


public class TargetComponent : MonoBehaviour
{
    [SerializeField] private bool vanishOnIdle;
    [SerializeField] private float minMovement;
    [SerializeField] private float idleTimeToVanishInSeconds;
    [SerializeField] private float movementMultiplier;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animation animation;
    [SerializeField] private Vector3 playerOffet;
    [SerializeField] private Grid gridComponent;


    Vector3 lastPosition;
    float lastMovement;
    private void Awake()
    {
        playerMovement.onPlayerMoved += OnPlayerMoved;
    }

    private void Update()
    {
        CheckVanishTime();   
    }

    void CheckVanishTime()
    {
        if (!vanishOnIdle)
            return;

        if(Time.time - lastMovement >= idleTimeToVanishInSeconds)
        {
            Vanish();
        }
    }
    void OnPlayerMoved(Vector2 target)
    {
        var position = playerMovement.GetPosition();
        var bearing = playerMovement.GetLastDirection();

        var newPosition = position + bearing;

        transform.position = gridComponent.CellToWorld(newPosition);

        lastMovement = Time.time;

        Appear();
    }

    void Appear()
    {
        if(spriteRenderer.color.a <= 0)
        {
            animation.Play("FadeIn");
        }
    }
    void Vanish()
    {
        if(spriteRenderer.color.a == 1)
        {
            animation.Play("FadeOut");
        }
    }
}
