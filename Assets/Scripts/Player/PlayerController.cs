using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    public void DisableInput()
    {
        playerInput.DeactivateInput();
    }

    public void EnableInput()
    {
        playerInput.ActivateInput();
    }
}
