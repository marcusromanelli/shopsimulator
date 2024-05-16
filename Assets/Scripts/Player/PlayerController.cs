using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private TargetComponent targetComponent;
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void DisableInput()
    {
        playerInput.DeactivateInput();
    }

    public void EnableInput()
    {
        playerInput.ActivateInput();
    }

    public void ForceMovement(Vector3 targetOffsetMovement, Action onFinish)
    {
        playerMovement.ForceMovement(targetOffsetMovement, onFinish);
    }
    public void OnReceivedMovement(InputAction.CallbackContext context)
    {
        var targetMovement = context.ReadValue<Vector2>();

        playerMovement.SetMovement(targetMovement);
    }
    public void OnReceivedInteraction(InputAction.CallbackContext context)
    {
        if (targetComponent == null)
            return;

        targetComponent.InteractWithTarget(this);
    }
}
