using ScriptsMisha.Player;
using UnityEngine;

namespace ScriptsMisha
{
    public class PlayerManager : MonoBehaviour
    {
        private Animator _animator;
        private InputManager _inputManager;
        private PlayerLocomotion _playerLocomotion;
        private PlayerActions _playerActions;

        public bool isInteracting;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _inputManager = GetComponent<InputManager>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
            _playerActions = GetComponent<PlayerActions>();
        }

        private void Update()
        {
            _inputManager.HandleAllInput();
        }

        private void FixedUpdate()
        {
            _playerLocomotion.HandleAllMovement();
            _playerActions.HandleAllActions();
        }
    }
}
