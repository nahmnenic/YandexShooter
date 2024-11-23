using System.Collections.Generic;
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
        private List<int> _pressedKeys = new List<int>();

        private void Update()
        {
            _pressedKeys.Clear();
            
            if (Keyboard.current.digit1Key.isPressed) _pressedKeys.Add(1);
            if (Keyboard.current.digit2Key.isPressed) _pressedKeys.Add(2);
            if (Keyboard.current.digit3Key.isPressed) _pressedKeys.Add(3);
            if (Keyboard.current.digit4Key.isPressed) _pressedKeys.Add(4);
            if (Keyboard.current.digit5Key.isPressed) _pressedKeys.Add(5);
            if (Keyboard.current.digit6Key.isPressed) _pressedKeys.Add(6);
            if (Keyboard.current.digit7Key.isPressed) _pressedKeys.Add(7);
            if (Keyboard.current.digit8Key.isPressed) _pressedKeys.Add(8);
            if (Keyboard.current.digit9Key.isPressed) _pressedKeys.Add(9);


            if (_pressedKeys.Count > 0)
            {
                _lastKeyPressed = _pressedKeys[_pressedKeys.Count - 1];

                if (_lastKeyPressed != _currentItem)
                {
                    SelectItems();
                }
            }
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