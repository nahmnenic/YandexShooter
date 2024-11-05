using ScriptsMisha.Components.Weapon;
using ScriptsMisha.Player;
using UnityEngine;

namespace ScriptsMisha
{
    public class InputManager : MonoBehaviour
    {
        private InputSystem _inputSystem;
        private PlayerLocomotion _playerLococmotion;
        private PlayerActions _playerActions;
        private WeaponComponent weapon;
        private AmmoSystem _ammoSystem;
        private CameraController _cam;
        private AnimatorManager _animatorManager;
        private Player1 _player;
        private Animator _anim;

        public Vector2 movementInput;
        public float moveAmount;
        public float verticalInput;
        public float horizontalalInput;

        public bool shift_Input;
        public bool space_Input;
        public bool e_Input;
        public bool lb_Input;
        public bool rb_Input;
        public bool ctrl_Input;
        public bool r_Input;

        private void Awake()
        {
            _animatorManager = GetComponent<AnimatorManager>();
            _cam = GetComponent<CameraController>();
            _anim = GetComponentInChildren<Animator>();
            _playerActions = GetComponent<PlayerActions>();
            _ammoSystem = GetComponentInChildren<AmmoSystem>();
            _playerLococmotion = GetComponent<PlayerLocomotion>();
            _player = GetComponent<Player1>();
            weapon = GetComponentInChildren<WeaponComponent>();
        }

        private void OnEnable()
        {
            if (_inputSystem == null)
            {
                _inputSystem = new InputSystem();
                
                _inputSystem.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

                _inputSystem.PlayerActions.Shift.performed += i => shift_Input = true;
                _inputSystem.PlayerActions.Shift.canceled += i => shift_Input = false;
                _inputSystem.PlayerActions.Aim.performed += i => rb_Input = true;
                _inputSystem.PlayerActions.Aim.canceled += i => rb_Input = false;
                _inputSystem.PlayerActions.Jump.performed += i => space_Input = true;
                _inputSystem.PlayerActions.Interaction.performed += i => e_Input = true;
                _inputSystem.PlayerActions.Fire.performed += i => lb_Input = true;
                _inputSystem.PlayerActions.Fire.canceled += i => lb_Input = false;
                _inputSystem.PlayerActions.Crouch.performed += i => ctrl_Input = true;
                _inputSystem.PlayerActions.Crouch.canceled += i => ctrl_Input = false;
                _inputSystem.PlayerActions.Reload.performed += i => r_Input = true;
            }
            
            _inputSystem.Enable();
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
        }

        public void HandleAllInput()
        {
            HandleMovementInput();
            HandleSprintingInput();
            HandleJumpingInput();
            HandleInteractionInput();
            HandleFireInput();
            HandleAimInput();
            HandleCrouchInput();
            HandleReloadInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalalInput) + Mathf.Abs(verticalInput));
            _anim.SetFloat("MoveAmount", moveAmount);
        }

        private void HandleSprintingInput()
        {
            if (shift_Input && moveAmount > 0.5f && _playerLococmotion.IsGrounded && verticalInput >= 0 && !_anim.GetBool("Reload"))
            {
                _playerLococmotion.IsSprinting = true;
                _anim.SetBool("IsRun", true);
            }
            else
            {
                _playerLococmotion.IsSprinting = false;
                _anim.SetBool("IsRun", false);
            }
        }

        private void HandleJumpingInput()
        {
            if (space_Input && !_anim.GetBool("Reload"))
            {
                space_Input = false;
                _playerLococmotion.HandleJumping();
            }
        }

        private void HandleCrouchInput()
        {
            if (ctrl_Input)
            {
                _playerLococmotion.IsCrouching = true;
            }
            else
            {
                _playerLococmotion.IsCrouching = false;
            }
        }

        private void HandleInteractionInput()
        {
            if (e_Input)
            {
                e_Input = false;
                _player.Interact();
            }
        }
        
        private void HandleFireInput()
        {
            if (lb_Input && !_anim.GetBool("Reload"))
            {
                weapon.IsFire = true;
                _anim.SetBool("IsShooting", true);
            }
            else
            {
                weapon.IsFire = false;
                _anim.SetBool("IsShooting", false);
            }
        }

        private void HandleAimInput()
        {
            if (rb_Input && !ctrl_Input && !_anim.GetBool("Reload"))
            {
                _playerLococmotion.IsAiming = true;
                _playerActions.IsAiming = true;
                _anim.SetBool("IsAim", true);
                _playerActions.HandleAiming();
            }
            else
            {
                _anim.SetBool("IsAim", false);
                _playerLococmotion.IsAiming = false;
                _playerActions.IsAiming = false;
            }
        }

        private void HandleReloadInput()
        {
            if (r_Input)
            {
                r_Input = false;
                _ammoSystem.Reload();
            }
        }
    }
}
