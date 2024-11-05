using System;
using UnityEngine;

namespace ScriptsMisha.Player
{
    public class PlayerActions : MonoBehaviour
    {
        private AnimatorManager _animatorManager;
        private Animator _animator;
        private InputManager _inputManager;
        private CameraController _camController;

        private static readonly int DieKey = Animator.StringToHash("isDead");

        [Header("OnlyWalk")]
        public bool IsAiming;

        private static readonly int Aiming = Animator.StringToHash("isAiming");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animatorManager = GetComponent<AnimatorManager>();
            _inputManager = GetComponent<InputManager>();
            _camController = GetComponent<CameraController>();
        }

        public void OnDie()
        {
            _animator.SetBool(DieKey, true);
            
        }
        
        public void HandleAllActions()
        {
            HandleAiming();
        }
        
        public void HandleAiming()
        {
            if (IsAiming)
            {
               // _animatorManager._animator.SetBool(Aiming,true);
            }
            else
            {
                //_animatorManager._animator.SetBool(Aiming,false);
            }
        }
    }
}