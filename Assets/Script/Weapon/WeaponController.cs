using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private GameObject _camera;

        [Range(0f, 1f)] public float horizontalSpread = 0.4f;
        [Range(0f, 1f)] public float verticalSpread = 0.2f;
        public int pelletsNumber = 6;
        private float _range = 70f;

        private void Awake()
        {
            _camera = GameObject.Find("MainCamera");
        }

        public void FixedUpdate()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                for (int pelletCount = 0; pelletCount < pelletsNumber; pelletCount++)
                {
                    ShotgunRay();
                }
            }
        }

        public void ShotgunRay()
        {
            RaycastHit pelletHit;
            GameObject target;

            Vector3 direction = _camera.transform.forward; // your initial aim.
            Vector3 spread = Vector3.zero;
            spread += _camera.transform.up * Random.Range(-verticalSpread, verticalSpread);
            spread += _camera.transform.right * Random.Range(-horizontalSpread, horizontalSpread);

            direction += spread.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(_camera.transform.position, direction, out pelletHit, _range))
            {
                target = pelletHit.transform.gameObject;
                if (target.gameObject.CompareTag("Foe"))
                {
                    Debug.Log("Enemy Hit !");
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
