using UnityEngine;


public class TargetComponent : MonoBehaviour
{
    [SerializeField] private bool vanishOnIdle;
    [SerializeField] private float idleTimeToVanishInSeconds;
    [SerializeField] private float movementMultiplier;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animation animation;


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
        Vector3 movementDirection = playerMovement.transform.position - lastPosition;

        transform.position = playerMovement.transform.position + (movementDirection * movementMultiplier);

        lastPosition = playerMovement.transform.position;

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
