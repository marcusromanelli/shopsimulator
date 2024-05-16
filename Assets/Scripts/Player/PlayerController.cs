using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInventory;
using static ShopItem;

[RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerInventory)), RequireComponent(typeof(PlayerHUD))]
public class PlayerController : MonoBehaviour
{
    public UnityEvent<CurrencyData, int> OnCurrencyChanged;
    public UnityEvent OnInventoryClosed;


    [SerializeField] private TargetComponent targetComponent;
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerInventory playerInventory;
    PlayerHUD playerHUD;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        playerInventory.OnCurrencyChanges.AddListener(OnCurrencyChanges);

        playerHUD = GetComponent<PlayerHUD>();
        playerHUD.Setup(CurrencyManager.Instance.GetElements(), GetCurrencyAmount);
    }
    private void OnCurrencyChanges(CurrencyData currencyData, int amount)
    {
        OnCurrencyChanged?.Invoke(currencyData, amount);

        playerHUD.UpdateCurrencyValue(currencyData, amount);
    }
    public void DisableInput()
    {
        playerInput.DeactivateInput();
    }

    public void EnableInput()
    {
        playerInput.ActivateInput();
    }

    public void OpenInventory()
    {
        playerHUD.OpenInventory(playerInventory, GetItemData);
        playerHUD.OnClosedInventory.AddListener(OnClosedInventory);
    }

    void OnClosedInventory()
    {
        OnInventoryClosed?.Invoke();
    }

    ShopItem GetItemData(string itemId)
    {
        return ItemManager.Instance.GetElementData(itemId);
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

    public void AddCurrency(CurrencyData currencyData, int amount)
    {
        playerInventory.AddCurrency(currencyData.GetId(), amount);
    }

    public void RemoveCurrency(CurrencyData currencyData, int amount)
    {
        playerInventory.RemoveCurrency(currencyData.GetId(), amount);
    }

    public void RemoveCurrency(Cost costData)
    {
        playerInventory.RemoveCurrency(costData.currency.GetId(), costData.amount);
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
