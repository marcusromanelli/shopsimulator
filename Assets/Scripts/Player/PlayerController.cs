using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInventory;
using static ShopItem;

[RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour
{
    public UnityEvent<string, int> OnCurrencyChanged;


    [SerializeField] private TargetComponent targetComponent;
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerInventory playerInventory;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        playerInventory.OnCurrencyChanges.AddListener(OnCurrencyChanges);
    }
    private void OnCurrencyChanges(string currencyId, int amount)
    {
        OnCurrencyChanged?.Invoke(currencyId, amount);
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

    public void AddItem(string itemId)
    {
        playerInventory.AddItem(itemId);
    }

    public void RemoveItem(string itemId)
    {
        playerInventory.RemoveItem(itemId);
    }
    public int GetCurrencyAmount(string currencyId)
    {
        return playerInventory.GetCurrencyAmount(currencyId);
    }
    public int GetCurrencyAmount(Cost costData)
    {
        return playerInventory.GetCurrencyAmount(costData);
    }
}
