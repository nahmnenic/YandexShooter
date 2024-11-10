using System;
using System.Linq;
using ScriptsMisha.Components;
using ScriptsMisha.Components.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsMisha.UI
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private Text _hp;
        [SerializeField] private Transform _player;
        [SerializeField] private Text _bullet;
        private SwitchGun _gun;

        private void Start()
        {
            _gun = FindObjectOfType<SwitchGun>();
        }

        private void Update()
        {
            var hp = _player.GetComponent<HealthComponent>();
            _hp.text = hp._health.ToString();
            _bullet.text = _gun.currentGun.GetComponent<AmmoSystem>().currentAmmo.ToString();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
