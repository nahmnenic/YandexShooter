using System;
using ScriptsMisha.Components.Weapon;
using UnityEngine;

namespace ScriptsMisha.UI
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Transform _selection;
        [SerializeField] private SwitchGun _switch;

        public void Select(int id)
        {
            _switch._guns[id].SetActive(true);
            _switch.Switch();
        }
        
        public void DeSelect(int id)
        {
            _switch._guns[id].SetActive(false);
            _switch.Switch();
        }
    }
}