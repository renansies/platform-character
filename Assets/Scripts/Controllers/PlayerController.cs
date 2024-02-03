using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Controllers
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        private PlayerInputActions inputActions;
        private bool isJumping;
        private bool isRunning;
        private bool isFiring;

        private void OnEnable()
        {
            inputActions = new PlayerInputActions();
            inputActions.GamePlay.Enable();
            inputActions.GamePlay.Jump.started += JumpStarted;
            inputActions.GamePlay.Jump.canceled += JumpCanceled;
            inputActions.GamePlay.Run.started += RunStarted;
            inputActions.GamePlay.Run.canceled += RunCanceled;
            inputActions.GamePlay.Fire.started += FireStarted;
            inputActions.GamePlay.Fire.canceled += FireCanceled;
        }

        private void OnDisable()
        {
            inputActions.GamePlay.Disable();
            inputActions.GamePlay.Jump.started -= JumpStarted;
            inputActions.GamePlay.Jump.canceled -= JumpCanceled;
            inputActions.GamePlay.Run.started -= RunStarted;
            inputActions.GamePlay.Run.canceled -= RunCanceled;
            inputActions.GamePlay.Fire.started -= FireStarted;
            inputActions.GamePlay.Fire.canceled -= FireCanceled;
            inputActions = null;
        }

        private void JumpStarted(CallbackContext obj)
        {
            isJumping = true;
        }
        private void JumpCanceled(CallbackContext obj)
        {
            isJumping = false;
        }
        public override bool RetrieveJumpInput(GameObject gameObject)
        {
            return isJumping;
        }
        
        public override float RetrieveMoveInput(GameObject gameObject)
        {
            return inputActions.GamePlay.Move.ReadValue<Vector2>().x;
        }

        private void RunStarted(CallbackContext obj)
        {
            isRunning = true;
        }
        private void RunCanceled(CallbackContext obj)
        {
            isRunning = false;
        }
        public override bool RetrieveRunInput(GameObject gameObject)
        {
            return isRunning;
        }

        private void FireStarted(CallbackContext obj)
        {
            isFiring = true;
        }
        private void FireCanceled(CallbackContext obj)
        {
            isFiring = false;
        }
        public override bool RetrieveFireInput(GameObject gameObject)
        {
            return isFiring;
        }
    }
}