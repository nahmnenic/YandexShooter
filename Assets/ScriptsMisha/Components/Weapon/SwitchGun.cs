using System;
using UnityEngine;

namespace ScriptsMisha.Components.Weapon
{
    public class SwitchGun : MonoBehaviour
    {
        public GameObject[] _guns;
        [HideInInspector] public GameObject currentGun;
        private InputManager _inputManager;
        private CameraController _camera;

        private void Start()
        {
            _inputManager = GetComponentInParent<InputManager>();
            _camera = GetComponentInParent<CameraController>();
            Switch();
        }

        public void Switch()
        {
            foreach (var gun in _guns)
            {
                if (gun.activeSelf)
                {
                    _inputManager._anim = gun.GetComponent<Animator>();
                    _inputManager.weapon = gun.GetComponent<WeaponComponent>();
                    _inputManager._ammoSystem = gun.GetComponent<AmmoSystem>();
                    _camera._currentGun = gun.transform;
                    currentGun = gun;
                }
            }
        }
    }
}
