using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
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
    [SerializeField] private Collider2D collider;


    private Trigger contactTrigger; 
    private float lastMovement;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        contactTrigger = collision.GetComponent<Trigger>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        contactTrigger = null;
    }

    public void InteractWithTarget(PlayerController playerController)
    {
        if (contactTrigger == null)
            return;

        contactTrigger.TryTriggerEvents(playerController);
    }
}
