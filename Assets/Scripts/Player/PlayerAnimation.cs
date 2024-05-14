using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerMovement;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;


    Vector2 lastDirection;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        playerMovement.onPlayerMoved += OnPlayerMoved;
        playerMovement.onPlayerStoppedMoving += OnPlayerStoppedMoving;
    }

    void Start()
    {
        
    }

    void OnPlayerMoved(Vector2 direction)
    {
        lastDirection = direction;

        animator.SetBool("Walking", true);
        animator.SetFloat("XSpeed", direction.x);
        animator.SetFloat("YSpeed", direction.y);

        var scale = transform.localScale;
        if (direction.x < 0) {
            scale.x = -1;
        }else if (direction.x > 0)
        {
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    void OnPlayerStoppedMoving()
    {
        animator.SetBool("Walking", false);
    }
}
