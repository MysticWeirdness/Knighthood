using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    private InputControls inputControls;
    private MovementController movementController;
    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        inputControls = new InputControls();
        playerAnimations = GetComponentInChildren<PlayerAnimations>();
    }
    private void OnEnable()
    {
        inputControls.Enable();
    }
    private void OnDisable()
    {
        inputControls.Disable();
    }

    private void Update()
    {
        // Passes Movement Vector2 to the movement script
        movementController.SetMovementVector(inputControls.Controller.Movement.ReadValue<Vector2>());

        // Turns the sprite in the correct direction
        playerAnimations.ChangeSpriteDirection(inputControls.Controller.Movement.ReadValue<Vector2>().x);

        // Based on input detects which state is the current state
        DetectState();


    }

    private void DetectState()
    {
        if(inputControls.Controller.Jump.ReadValue<float>() > 0)
        {
            playerAnimations.SwitchState(PlayerAnimations.State.jumping);
        }
        if (inputControls.Controller.Movement.ReadValue<Vector2>() == Vector2.zero && movementController.Grounded())
        {
            playerAnimations.SwitchState(PlayerAnimations.State.idle);
        }
        switch (playerAnimations.GetCurrentState())
        {
            case PlayerAnimations.State.jumping:
                return;
        }
        if (inputControls.Controller.Movement.ReadValue<Vector2>() != Vector2.zero && inputControls.Controller.Run.ReadValue<float>() == 0)
        {
            playerAnimations.SwitchState(PlayerAnimations.State.walking);
        }
        else if (inputControls.Controller.Movement.ReadValue<Vector2>() != Vector2.zero && inputControls.Controller.Run.ReadValue<float>() > 0)
        {
            playerAnimations.SwitchState(PlayerAnimations.State.running);
        }
    }
}
