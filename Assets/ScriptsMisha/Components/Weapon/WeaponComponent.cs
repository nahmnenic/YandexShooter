using System.Collections;
using UnityEngine;

namespace ScriptsMisha.Components.Weapon
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private bool _addBulletSpread = true;
        [SerializeField] private Vector3 _bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private Transform BulletSpawnPoint;
        [SerializeField] private ParticleSystem ImpactParticleSystem;
        [SerializeField] private TrailRenderer BulletTrail;

        private CameraController _camCont;
        private Animator _anim;
        private AmmoSystem _ammo;
        [SerializeField] private ParticleSystem _particle;

        [SerializeField] private Transform _midBody;
        [SerializeField] private LayerMask _mask;
        
        [Header("Fire Rate")] 
        [SerializeField] private float fireRate;
        [SerializeField] private bool semiAuto;
        public bool IsFire;
        private float _fireRateTimer;

        [Header("Bullet Properties")] 
        public int damage;
        [SerializeField] private Transform barrelPos;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _ammo = GetComponent<AmmoSystem>();
            _camCont = GetComponentInParent<CameraController>();
        }

        private void Start()
        {
            _fireRateTimer = fireRate;
        }

        private void Update()
        {
            if (ShouldFire()) Fire();
        }

        private bool ShouldFire()
        {
            _fireRateTimer += Time.deltaTime;
            if (_fireRateTimer < fireRate) return false;
            if (_ammo.currentAmmo == 0) return false;
            if (semiAuto && IsFire) return true;
            if (!semiAuto && IsFire) return true;
            return false;
        }

        private void Fire()
        {
            _anim.SetBool("IsShooting", true);
            barrelPos.LookAt(_camCont.aimPos);
            _particle.Play();
            Vector3 fireDirection = GetDirection();
            RaycastHit hit;
            if (Physics.Raycast(_midBody.transform.position, fireDirection, out hit, _mask))
            {
                _ammo.currentAmmo--;
                _fireRateTimer = 0;
                var enemy = hit.transform.TryGetComponent(out HealthComponent healthComponent);
                if (enemy)
                {
                    healthComponent.ModifyHealth(damage);
                }
                else
                {
                    TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit));
                }
            }
        }

        private Vector3 GetDirection()
        {
            Vector3 direction = transform.forward;

            if (_addBulletSpread)
            {
                direction += new Vector3(
                    Random.Range(-_bulletSpreadVariance.x, _bulletSpreadVariance.x),
                    Random.Range(-_bulletSpreadVariance.y, _bulletSpreadVariance.y),
                    Random.Range(-_bulletSpreadVariance.z, _bulletSpreadVariance.z)
                );

                direction.Normalize();
            }

            return direction;
        }

        private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
        {
            float time = 0;
            Vector3 StartPos = Trail.transform.position;

            while (time < 1)
            {
                Trail.transform.position = Vector3.Lerp(StartPos, hit.point, time);
                time += Time.deltaTime / Trail.time;

                yield return null;
            }

            Trail.transform.position = hit.point;
            Instantiate(ImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
            
            Destroy(Trail.gameObject, Trail.time);
        }
    }
}