using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScriptsMisha.UI
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _horizontalGroup;
        public Transform[] _items;
        private int _currentItem;
        private int _lastKeyPressed;
        private InputSystem _inputSystem;

        private void OnEnable()
        {
            if (_inputSystem == null)
            {
                _inputSystem = new InputSystem();

                _inputSystem.PlayerActions.Numbers.performed += context => CheckKeyPressed();
            }

            _inputSystem.Enable();
        }

        private void CheckKeyPressed()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame) _lastKeyPressed = 1;
            else if (Keyboard.current.digit2Key.wasPressedThisFrame) _lastKeyPressed = 2;
            else if (Keyboard.current.digit3Key.wasPressedThisFrame) _lastKeyPressed = 3;
            else if (Keyboard.current.digit4Key.wasPressedThisFrame) _lastKeyPressed = 4;
            else if (Keyboard.current.digit5Key.wasPressedThisFrame) _lastKeyPressed = 5;
            else if (Keyboard.current.digit6Key.wasPressedThisFrame) _lastKeyPressed = 6;
            else if (Keyboard.current.digit7Key.wasPressedThisFrame) _lastKeyPressed = 7;
            else if (Keyboard.current.digit8Key.wasPressedThisFrame) _lastKeyPressed = 8;
            else if (Keyboard.current.digit9Key.wasPressedThisFrame) _lastKeyPressed = 9;
            else return;

            if (_lastKeyPressed == _currentItem) return;
            SelectItems();
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
        }

        private void SelectItems()
        {
            if(_lastKeyPressed > _items.Length) return;
            if(_currentItem != 0) _items[_currentItem - 1].GetComponent<ItemWidget>().DeSelect(_currentItem - 1);
                
            _currentItem = _lastKeyPressed;
            _items[_lastKeyPressed-1].GetComponent<ItemWidget>().Select(_lastKeyPressed-1);
        }
    }
}