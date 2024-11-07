using System;
using ScriptsMisha.UI;
using UnityEngine;

namespace ScriptsMisha.Components.Weapon
{
    public class AmmoSystem : MonoBehaviour
    {
        public int currentAmmo;
        public int clipSize;
        public int extraAmmo;
        private Animator _anim;
        

        [Header("Fire")]
        private float _progressFire;

        [Header("Reload")]
        private float _progressReload;

        private UIGame _ui;

        private void Start()
        {
            currentAmmo = clipSize;
            _ui = FindObjectOfType<UIGame>();
            _anim = GetComponent<Animator>();
        }

        public void Reload()
        {
            if (extraAmmo >= clipSize)
            {
                int ammoToReaload = clipSize - currentAmmo;
                extraAmmo -= ammoToReaload;
                currentAmmo += ammoToReaload;
                _anim.SetBool("Reload", true);
            }
            else if (extraAmmo > 0)
            {
                if (extraAmmo + currentAmmo > clipSize)
                {
                    int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                    extraAmmo = leftOverAmmo;
                    currentAmmo = clipSize;
                    _anim.SetBool("Reload", true);
                }
                else
                {
                    currentAmmo += extraAmmo;
                    extraAmmo = 0;
                    _anim.SetBool("Reload", true);
                }
            }
        }

        public void ReloadAimation()
        {
            _anim.SetBool("Reload", false);
        }
    }
}
