using System;
using UnityEngine;

namespace ScriptsMisha.Components.Weapon
{
    public class SwitchGun : MonoBehaviour
    {
        public GameObject[] _guns;
        public GameObject _currentGun;
        private InputManager _animator;
        private CameraController _camera;

        private void Start()
        {
            _animator = GetComponentInParent<InputManager>();
            _camera = GetComponentInParent<CameraController>();
            Switch();
        }

        public void Switch()
        {
            foreach (var gun in _guns)
            {
                if (gun.activeSelf)
                {
                    _animator._anim = gun.GetComponent<Animator>();
                    _animator.weapon = gun.GetComponent<WeaponComponent>();
                    _animator._ammoSystem = gun.GetComponent<AmmoSystem>();
                    _camera._currentGun = gun.transform;
                    //_currentGun = gun;
                }
            }
        }
    }
}
