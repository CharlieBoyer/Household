using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private GameObject _camera;
        private spreadCollider _spreadCollider;
        private ParticleSystem _muzzleFlash;
        // public GameObject ammoPrefab;
        
        /* ShootRays */
        [HideInInspector] public int pelletsNumber = 6;
        [HideInInspector] public float ammoRange = 70f;
        [Range(0f, 1f)] public float horizontalSpread = 0.4f;
        [Range(0f, 1f)] public float verticalSpread = 0.2f;

        private void Awake()
        {
            _camera = GameObject.Find("CrossHair");
            _spreadCollider = GameObject.Find("spreadCollider").GetComponent<spreadCollider>();
            _muzzleFlash = GameObject.Find("MuzzleFlash").GetComponent<ParticleSystem>();
        }

        public void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootClassic(); // ShootRays();
            }
        }

        private void ShootClassic()
        {
            _muzzleFlash.Play();
            if (_spreadCollider.triggered)
            {
                Destroy(_spreadCollider.foe.gameObject);
            }
        }

        private void ShootRays()
        {
            for (int pelletCount = 0; pelletCount < pelletsNumber; pelletCount++)
            {
                ShotgunRay();
            }
        }

        private void ShotgunRay()
        {
            RaycastHit pelletHit;
            GameObject target;

            Vector3 direction = _camera.transform.forward; // initial aim.
            Vector3 spread = Vector3.zero;
            spread += _camera.transform.up * Random.Range(-verticalSpread, verticalSpread);
            spread += _camera.transform.right * Random.Range(-horizontalSpread, horizontalSpread);

            direction += spread.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(_camera.transform.position, direction, out pelletHit, ammoRange))
            {
                target = pelletHit.transform.gameObject;
                if (target.gameObject.CompareTag("Foe"))
                {
                    Destroy(target.gameObject);
                }
                Debug.DrawLine(_camera.transform.position, pelletHit.point, Color.green, 5f);
            }
            else
            {
                Debug.DrawLine(_camera.transform.position, pelletHit.normal, Color.red, 5f);
            }
        }
    }
}
