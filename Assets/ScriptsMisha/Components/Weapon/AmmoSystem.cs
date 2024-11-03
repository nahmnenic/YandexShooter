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
        

        [Header("Fire")]
        public bool Fire;
        private float _progressFire;
        public float timeFire;
        
        [Header("Reload")]
        private float _progressReload;
        public float timeReload;

        private UIGame _ui;

        private void Start()
        {
            currentAmmo = clipSize;
            _ui = FindObjectOfType<UIGame>();
        }

        public void Reload()
        {
            if (extraAmmo >= clipSize)
            {
                int ammoToReaload = clipSize - currentAmmo;
                extraAmmo -= ammoToReaload;
                currentAmmo += ammoToReaload;
            }
            else if (extraAmmo > 0)
            {
                if (extraAmmo + currentAmmo > clipSize)
                {
                    int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                    extraAmmo = leftOverAmmo;
                    currentAmmo = clipSize;
                }
                else
                {
                    currentAmmo += extraAmmo;
                    extraAmmo = 0;
                }
            }
        }
    }
}
